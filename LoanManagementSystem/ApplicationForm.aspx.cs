using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class ApplicationForm : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-J4A1LCO\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;User ID=user;Password=user";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        txtLoanID.Text = GenerateUniqueNumber();

        }
        BindDDLBorrowes();
    }


    // This method will populate all borrowers to the dropdown list as value 
    protected void BindDDLBorrowes()
    {
        string query = "SELECT BorrowerID, FullName FROM Borrowers";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(query, conn);

        conn.Open();

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            string id = reader["BorrowerID"].ToString();
            string name = reader["FullName"].ToString();

            string stringView = name + " - " + id;

            // Create a new ListItem with name as the text and value as the value
            ListItem item = new ListItem(stringView, id);

            // Add the item to the dropdown list
            // Add the custom CSS class to the ListItem
            item.Attributes["class"] = "dropdown-item";
            ddlBorrowers.Items.Add(item);
        }

        reader.Close();

    }

    // A helper method to generate Unique ID for Borrower ID
    public string GenerateUniqueNumber()
    {
        long timestamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond; // Get current timestamp

        string uniqueNumber = timestamp.ToString();
        uniqueNumber = uniqueNumber.Substring(uniqueNumber.Length - 10); // Take the rightmost 10 digits

        return uniqueNumber;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string borrowerId = ddlBorrowers.SelectedValue;
        string loanId = txtLoanID.Text;
        string status = ddlStatus.SelectedValue;
        bool hasErr = false;


        if (string.IsNullOrEmpty(borrowerId) || string.IsNullOrEmpty(loanId) || string.IsNullOrEmpty(status) ||
           string.IsNullOrEmpty(txtLoanTerm.Text) || string.IsNullOrEmpty(txtNumOfRepayments.Text) || string.IsNullOrEmpty(txtInterestRate.Text) ||
           string.IsNullOrEmpty(txtPaymentPerMonth.Text) || string.IsNullOrEmpty(txtTotalLoanAmnt.Text) || string.IsNullOrEmpty(txtStartDate.Text) ||
           string.IsNullOrEmpty(txtEndDate.Text)
            )
        {
            lblgeneralErr.Text = "Input fields cannot be empty";
            hasErr = true;
        } else {
            lblgeneralErr.Text = "";
        }


        if (hasErr) return;

        int loanTerm = Convert.ToInt32(txtLoanTerm.Text);

        if (loanTerm == 0)
        {
            lblLoanTermErr.Text = "Loan Term cannot be 0";
            hasErr = true;
        } else
        {
            lblLoanTermErr.Text = "";
        }

        int numOfRepayments = Convert.ToInt32(txtNumOfRepayments.Text);

        if (numOfRepayments == 0)
        {
            lblNumOfRepaymentsErr.Text = "Number Of Repayments cannot be 0";
            hasErr = true;
        } else
        {
            lblLoanTermErr.Text = "";
        }

        decimal interestRate = Convert.ToDecimal(txtInterestRate.Text.Trim());
        decimal loanAmount = Convert.ToDecimal(txtAmountRequested.Text.Trim());

        if (loanAmount == 0)
        {
            lblTotalLoanAmntErr.Text = "Loan amount cannot be 0";
            hasErr = true;
        } else
        {
            lblTotalLoanAmntErr.Text = "";
        }


        decimal monthlyPayment = Convert.ToDecimal(txtPaymentPerMonth.Text.Trim());

        if (monthlyPayment == 0)
        {
            lblPaymentPerMonthErr.Text = "Monthy payment cannot be 0";
            hasErr = true;
        } else
        {
            lblPaymentPerMonthErr.Text = "";
        }

        decimal totalLoanAmount = Convert.ToDecimal(txtTotalLoanAmnt.Text.Trim());


        if (totalLoanAmount == 0)
        {
            lblPaymentPerMonthErr.Text = "Total loan amount cannot be 0";
            hasErr = true;
        } else
        {
            lblPaymentPerMonthErr.Text = "";
        }

        DateTime startDate = DateTime.Parse(txtStartDate.Text);
        DateTime endDate = DateTime.Parse(txtEndDate.Text);
        bool istermsChecked = termsCheckbox.Checked;

        if (!istermsChecked)
        {
            termsCheckboxErr.Text = "Please accept terms and condition!";
            hasErr = true;
        }
        else
        {
            termsCheckboxErr.Text = "";
        }
        

        decimal balance = CalculateBalance(loanId, totalLoanAmount);
        decimal paid = CalculateTotalPayments(loanId);

        SqlConnection conn = new SqlConnection(connectionString);

        string loanQuery = "SELECT * FROM Loans WHERE LoanID = @loanId";

        SqlCommand cmd = new SqlCommand(loanQuery, conn);

        cmd.Parameters.AddWithValue("@loanId", loanId);
        conn.Open();

        SqlDataReader loanReader = cmd.ExecuteReader();

        if (loanReader.Read())
        {
            lblLoanIDErr.Text = "This loan is already exist!";
            hasErr = true;
        }

        if (hasErr) return;

        loanReader.Close();

        string addLoan = "INSERT INTO Loans (BorrowerID, LoanID, LoanAmount, LoanTerm, InterestRate, TotalLoanAmount, MonthlyPayment, StartDate, EndDate, NumOfRepayments, Status, Balance, Paid) " +
            "VALUES (@borrowerId, @loanId, @loanAmount, @loanTerm, @interestRate, @totalLoanAmount, @monthlyPayment, @startDate, @endDate, @numOfRepayments, @status, @balance, @paid)";

        SqlCommand cmd1 = new SqlCommand(addLoan, conn);

        cmd1.Parameters.AddWithValue("@borrowerId", borrowerId);
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


        cmd1.ExecuteNonQuery();


        conn.Close();

        Response.Redirect("Loans.aspx");
    }


}