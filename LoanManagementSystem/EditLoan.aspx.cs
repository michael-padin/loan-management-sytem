using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class EditLoan : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-TDH7QKT\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;Persist Security Info=True;User ID=user;Password=user";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            PopulateData();
        }
    }

    protected void PopulateData()
    {
        string loanId = Request.QueryString["loanId"];

        if (String.IsNullOrEmpty(loanId))
        {
            Response.Redirect("Loans.aspx");
        }

        string borrowerQuery = @"SELECT Loans.*, Borrowers.FullName " +
                               "FROM Loans INNER JOIN Borrowers ON Loans.BorrowerID = Borrowers.BorrowerID " +
                               "WHERE Loans.LoanID = @loanId";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(borrowerQuery, con);
        cmd.Parameters.AddWithValue("@loanId", loanId);

        con.Open();

        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            txtLoanID.Text = loanId;
            txtBorrower.Text = reader["FullName"].ToString();   
            txtAmountRequested.Text = reader["LoanAmount"].ToString();
            txtLoanTerm.Text = reader["LoanTerm"].ToString();
            txtInterestRate.Text = reader["InterestRate"].ToString();
            txtTotalLoanAmnt.Text = reader["LoanTerm"].ToString();
            txtPaymentPerMonth.Text = reader["MonthlyPayment"].ToString();
            DateTime startDate = Convert.ToDateTime(reader["startDate"]);
            DateTime endDate = Convert.ToDateTime(reader["endDate"]);
            txtStartDate.Text = startDate.ToString("yyyy-MM-dd");
            txtEndDate.Text = endDate.ToString("yyyy-MM-dd");
            txtNumOfRepayments.Text = reader["NumOfRepayments"].ToString();
            ddlStatus.SelectedValue = reader["Status"].ToString();

        }
        else
        {
            Response.Redirect("Loans.aspx");
        }

        con.Close();

    }

    public decimal CalculateBalance(string loanID, decimal totalLoanAmount)
    {
        // Initialize the balance variable
        decimal balance = 0;

        SqlConnection connection = new SqlConnection(connectionString);

        SqlCommand paymentCommand = new SqlCommand("SELECT SUM(PaymentAmount) FROM Payments WHERE LoanID = @LoanID", connection);
        paymentCommand.Parameters.AddWithValue("@LoanID", loanID);

        connection.Open();


        SqlDataReader paymentReader = paymentCommand.ExecuteReader();

        if (paymentReader.Read())
        {
            object paymentResult = paymentReader.GetValue(0);
            decimal paymentAmountSum = paymentResult != DBNull.Value && paymentResult != null ? Convert.ToDecimal(paymentResult) : 0;

            balance = totalLoanAmount - paymentAmountSum;
        }

        connection.Close();

        return balance;

    }

    public decimal CalculateTotalPayments(string loanID)
    {
        decimal totalPayments = 0;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand paymentCommand = new SqlCommand("SELECT SUM(PaymentAmount) FROM Payments WHERE LoanID = @LoanID", connection);
            paymentCommand.Parameters.AddWithValue("@LoanID", loanID);
            object result = paymentCommand.ExecuteScalar();

            if (result != DBNull.Value && result != null)
            {
                totalPayments = Convert.ToDecimal(result);
            }

            connection.Close();
        }

        return totalPayments;
    }   

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string borrwerId = txtBorrower.Text ;
        string loanId = txtLoanID.Text;
        decimal loanAmount = Convert.ToDecimal(txtAmountRequested.Text.Trim());
        int loanTerm = Convert.ToInt32(txtLoanTerm.Text);
        decimal interestRate = Convert.ToDecimal(txtInterestRate.Text.Trim());
        decimal monthlyPayment = Convert.ToDecimal(txtPaymentPerMonth.Text.Trim());
        decimal totalLoanAmount = Convert.ToDecimal(txtTotalLoanAmnt.Text.Trim());
        DateTime startDate = DateTime.Parse(txtStartDate.Text);
        DateTime endDate = DateTime.Parse(txtEndDate.Text);
        int numOfRepayments = Convert.ToInt32(txtNumOfRepayments.Text);
        string status = ddlStatus.SelectedValue;

        decimal balance = CalculateBalance(loanId, totalLoanAmount);
        decimal paid = CalculateTotalPayments(loanId);

        string query = "UPDATE Loans SET loanID = @loanID, LoanAmount = @loanAmount, LoanTerm = @loanTerm, InterestRate = @interestRate, " +
            "MonthlyPayment = @monthlyPayment, StartDate = @startDate, EndDate = @endDate, NumOfRepayments = @numOfRepayments, Status = @status," +
            "Balance = @balance, Paid = @paid WHERE LoanID = @loanId";

        SqlConnection conn = new SqlConnection(connectionString);

        SqlCommand cmd1 = new SqlCommand(query, conn);

        cmd1.Parameters.AddWithValue("@loanId", loanId);
        cmd1.Parameters.AddWithValue("@loanAmount", loanAmount);
        cmd1.Parameters.AddWithValue("@loanTerm", loanTerm);
        cmd1.Parameters.AddWithValue("@interestRate", interestRate);
        cmd1.Parameters.AddWithValue("@totalLoanAmount", totalLoanAmount);
        cmd1.Parameters.AddWithValue("@monthlyPayment", monthlyPayment);
        cmd1.Parameters.AddWithValue("@startDate", startDate);
        cmd1.Parameters.AddWithValue("@endDate", endDate);
        cmd1.Parameters.AddWithValue("@numOfRepayments", numOfRepayments);
        cmd1.Parameters.AddWithValue("@status", status);
        cmd1.Parameters.AddWithValue("@paid", paid);
        cmd1.Parameters.AddWithValue("@balance", balance);

        conn.Open();

        cmd1.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("Loans.aspx");
    }

}