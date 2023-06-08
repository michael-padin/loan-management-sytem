using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentForm : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-TDH7QKT\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;Persist Security Info=True;User ID=user;Password=user";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPayment();
            
        }
    }

    protected void BindPayment()
    {
        txtPaymentID.Text = GenerateUniqueNumber();
        string loanID = Request.QueryString["loanId"];
        txtLoanID.Text = loanID;


        if(string.IsNullOrEmpty(loanID) || string.IsNullOrEmpty(txtPaymentID.Text))
        {
            Response.Redirect("Payments.aspx");
        }

        string lastPaymentQuery = "SELECT * FROM Loans WHERE LoanID = @loanId";

        // Create a new DataTable
        SqlConnection conn = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand(lastPaymentQuery, conn);

        cmd.Parameters.AddWithValue("@loanId", loanID);

        conn.Open();

        SqlDataReader lastPaymentReader = cmd.ExecuteReader();

        if (lastPaymentReader.Read()) {

            DateTime lastPayment;

            if (lastPaymentReader["LastPaymentDate"] != DBNull.Value)
            {
                lastPayment = Convert.ToDateTime(lastPaymentReader["LastPaymentDate"]);
            } else
            {
                lastPayment = Convert.ToDateTime(lastPaymentReader["StartDate"]);
            
            }
            DateTime newDate = lastPayment.AddMonths(1);
            txtPaymentDate.Text = newDate.ToString("yyyy-MM-dd");
            txtPaymentAmount.Text = lastPaymentReader["MonthlyPayment"].ToString();
            txtBorrowerID.Text = lastPaymentReader["BorrowerID"].ToString();
        }
        else { 
        }

    }

    public string GenerateUniqueNumber()
    {
        long timestamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond; // Get current timestamp

        string uniqueNumber = timestamp.ToString();
        uniqueNumber = uniqueNumber.Substring(uniqueNumber.Length - 10); // Take the rightmost 10 digits

        return "P"+uniqueNumber;
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

    public decimal CalculateBalance(string loanID)
    {
        // Initialize the balance variable
        decimal balance = 0;

        SqlConnection connection = new SqlConnection(connectionString);

        SqlCommand balanceCommand = new SqlCommand("SELECT TotalLoanAmount FROM Loans WHERE LoanID = @LoanID", connection);
        balanceCommand.Parameters.AddWithValue("@LoanID", loanID);

        connection.Open();

        SqlDataReader balanceReader = balanceCommand.ExecuteReader();

        if (balanceReader.Read())
        {
            object balanceResult = balanceReader.GetValue(0);
            decimal totalLoanAmount = balanceResult != DBNull.Value && balanceResult != null ? Convert.ToDecimal(balanceResult) : 0;

            SqlCommand paymentCommand = new SqlCommand("SELECT SUM(PaymentAmount) FROM Payments WHERE LoanID = @LoanID", connection);
            paymentCommand.Parameters.AddWithValue("@LoanID", loanID);

            balanceReader.Close(); // Close the previous reader before using the connection for a new query

            SqlDataReader paymentReader = paymentCommand.ExecuteReader();

            if (paymentReader.Read())
            {
                object paymentResult = paymentReader.GetValue(0);
                decimal paymentAmountSum = paymentResult != DBNull.Value && paymentResult != null ? Convert.ToDecimal(paymentResult) : 0;

                balance = totalLoanAmount - paymentAmountSum;
            }

            paymentReader.Close();
        }

        connection.Close();

        return balance;
    }


    protected void btnSubmt_Click(object sender, EventArgs e) {
        
        string loanId = txtLoanID.Text;
        string paymentId = txtPaymentID.Text;
        string borrowerId= txtBorrowerID.Text;
        

        bool hasErr = false;
       

        if(string.IsNullOrEmpty(loanId) || string.IsNullOrEmpty(paymentId) || string.IsNullOrEmpty(txtPaymentDate.Text) || string.IsNullOrEmpty(txtPaymentAmount.Text) || string.IsNullOrEmpty(borrowerId))
        {
            lblGeneralErr.Text = "Input fields cannot be empty";
            hasErr = true;
        }
        else
        {
            lblGeneralErr.Text = "";
        }

        if (hasErr) return;

        DateTime paymentDate = Convert.ToDateTime(txtPaymentDate.Text);
        decimal paymentAmount = Convert.ToDecimal(txtPaymentAmount.Text);

        string paymentQuery = "INSERT INTO Payments (PaymentID, LoanID, BorrowerID, PaymentAmount, PaymentDate) " +
            "VALUES (@paymentId, @loanId, @borrowerId, @paymentAmount, @paymentDate)";

        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        SqlCommand cmd = new SqlCommand(paymentQuery, conn);
        cmd.Parameters.AddWithValue("@paymentId", paymentId);
        cmd.Parameters.AddWithValue("@loanId", loanId);
        cmd.Parameters.AddWithValue("@borrowerId", borrowerId);
        cmd.Parameters.AddWithValue("@paymentDate", paymentDate);
        cmd.Parameters.AddWithValue("@paymentAmount", paymentAmount);

        cmd.ExecuteNonQuery();


        decimal balance = CalculateBalance(loanId);
        decimal paid = CalculateTotalPayments(loanId);

        string loanQuery = "UPDATE Loans SET Paid = @paid, Balance = @balance, LastPaymentDate = @paymentDate WHERE LoanID = @loanId";

        SqlCommand cmd1 = new SqlCommand(loanQuery, conn);
        cmd1.Parameters.AddWithValue("@paid", paid);
        cmd1.Parameters.AddWithValue("@paymentDate", paymentDate);
        cmd1.Parameters.AddWithValue("@loanId", loanId);
        cmd1.Parameters.AddWithValue("@balance", balance);
        cmd1.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("Payments.aspx");

    }

}