using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class CustomerForm : System.Web.UI.Page
{

    string connectionString = "Data Source=DESKTOP-J4A1LCO\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;User ID=user;Password=user";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // auto fill id textbox with the id generator method
            txtBorrowerID.Text = GenerateUniqueNumber();
        }
    }

    // A helper method to generate Unique ID for Borrower ID
    public string GenerateUniqueNumber()
    {
        long timestamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond; // Get current timestamp

        string uniqueNumber = timestamp.ToString();
        uniqueNumber = uniqueNumber.Substring(uniqueNumber.Length - 10); // Take the rightmost 10 digits

        return uniqueNumber;
    }

    public static bool IsValidFullName(string fullName)
    {
        string pattern = @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(fullName);
    }


    // Submit or Add Borrower Button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string id = txtBorrowerID.Text;
        string name = txtCustomerName.Text;
        string mobile = txtContact.Text;
        string email = txtEmail.Text;
        string address = txtAddress.Text;
        string employer = txtEmployerName.Text;
        string occupation = txtPosition.Text;
        bool hasErr = false;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) 
            || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(email) || 
            string.IsNullOrEmpty(address) || string.IsNullOrEmpty(employer) || 
            string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(txtMonthlyIncome.Text))
        {

            lblGeneralErr.Text = "Input fields cannot be empty";
            hasErr = true;
        }else
        {
            lblGeneralErr.Text = "";
        }

        if (mobile.Length != 11)
        {
            lblContactErr.Text = "Mobile Number length should be equal to 11";
            hasErr = true;
        }else
        {
            lblContactErr.Text = "";
        }

        if (!IsValidFullName(name)) {
            lblCustomerNameErr.Text = "Please enter a valid full name.";
        } else
        {
            lblCustomerNameErr.Text = "";
        }


        decimal monthlyIncome = Convert.ToDecimal(txtMonthlyIncome.Text);

        if (monthlyIncome == 0)
        {
            lblMonthlyIncomeErr.Text = "Monthly Income cannot be zero";
            hasErr = true;
        } else
        {
            lblMonthlyIncomeErr.Text = "";
        }


        string borrowerQuery = "SELECT * FROM Borrowers WHERE BorrowerID = @borrowerId";
        
        SqlConnection conn = new SqlConnection(connectionString);

        SqlCommand borrowerCmd = new SqlCommand(borrowerQuery, conn);

        borrowerCmd.Parameters.AddWithValue("@borrowerId", id);

        conn.Open();

        SqlDataReader borrowerReader = borrowerCmd.ExecuteReader();

        if (borrowerReader.Read())
        {
            lblBorrowerIDErr.Text = "This Borrower Already exist!";
            hasErr = true;
        } else
        {
            lblBorrowerIDErr.Text = "";
        }

        if (hasErr) return;


        borrowerReader.Close(); 

        
        string query =

            "INSERT INTO Borrowers (BorrowerID, FullName, Email, Mobile, Address, Employer, MonthlyIncome, Occupation) VALUES (@id, @name, @email, @mobile, @address, @employer, @monthlyincome, @occupation)";


        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@mobile", mobile);
        cmd.Parameters.AddWithValue("@address", address);
        cmd.Parameters.AddWithValue("@employer", employer);
        cmd.Parameters.AddWithValue("@monthlyincome", monthlyIncome);
        cmd.Parameters.AddWithValue("@occupation", occupation);


        cmd.ExecuteNonQuery();


        conn.Close();

        Response.Redirect("Borrowers.aspx");

    }
}