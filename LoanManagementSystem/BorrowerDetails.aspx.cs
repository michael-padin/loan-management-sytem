using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerDetails : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-J4A1LCO\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;User ID=user;Password=user";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            PopulateData();
        }

    }


    protected void PopulateData()
    {
        string borrowerId  = Request.QueryString["borrowerId"];
        decimal monthlyIncome = Convert.ToDecimal(Request.QueryString["monthlyIncome"]);
        string name = Request.QueryString["name"];
        string address = Request.QueryString["address"];
        string email = Request.QueryString["email"];
        string mobile = Request.QueryString["mobile"];
        string employer = Request.QueryString["employer"];
        string occupation = Request.QueryString["occupation"];



        if (String.IsNullOrEmpty(borrowerId))
        {
            Response.Redirect("Borrowers.aspx");
        }

        txtBorrowerID.Text = borrowerId;
        txtMonthlyIncome.Text = monthlyIncome.ToString();
        txtCustomerName.Text = name;
        txtAddress.Text = address;
        txtEmail.Text = email;
        txtContact.Text = mobile;
        txtEmployerName.Text = employer;
        txtPosition.Text = occupation;
    }

    public static bool IsValidFullName(string fullName)
    {
        string pattern = @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(fullName);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string borrowerId = Request.QueryString["borrowerId"];
        string name = txtCustomerName.Text;
        string address = txtAddress.Text;
        string email = txtEmail.Text;
        string mobile = txtContact.Text;
        string employer = txtEmployerName.Text;
        string occupation = txtPosition.Text;
        bool hasErr = false;

        if (string.IsNullOrEmpty(borrowerId) || string.IsNullOrEmpty(name)
        || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(email) ||
        string.IsNullOrEmpty(address) || string.IsNullOrEmpty(employer) ||
        string.IsNullOrEmpty(occupation) || string.IsNullOrEmpty(txtMonthlyIncome.Text))
        {

            lblGeneralErr.Text = "Input fields cannot be empty";
            hasErr = true;
        }
        else
        {
            lblGeneralErr.Text = "";
        }

        if (mobile.Length != 11)
        {
            lblContactErr.Text = "Mobile Number length should be equal to 11";
            hasErr = true;
        }
        else
        {
            lblContactErr.Text = "";
        }

        if (!IsValidFullName(name))
        {
            lblCustomerNameErr.Text = "Please enter a valid full name.";
        }
        else
        {
            lblCustomerNameErr.Text = "";
        }


        decimal monthlyIncome = Convert.ToDecimal(txtMonthlyIncome.Text);

        if (monthlyIncome == 0)
        {
            lblMonthlyIncomeErr.Text = "Monthly Income cannot be zero";
            hasErr = true;
        }
        else
        {
            lblMonthlyIncomeErr.Text = "";
        }

        if (hasErr) return;

        string query = "UPDATE Borrowers SET FullName = @name, Email = @email, Address = @address, Mobile = @mobile, Employer = @employer, Occupation = @occupation WHERE BorrowerID = @borrowerid";

        SqlConnection conn = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@borrowerid", borrowerId);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@mobile", mobile);
        cmd.Parameters.AddWithValue("@address", address);
        cmd.Parameters.AddWithValue("@employer", employer);
        cmd.Parameters.AddWithValue("@monthlyIncome", monthlyIncome);
        cmd.Parameters.AddWithValue("@occupation", occupation);

        conn.Open();

        cmd.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("Borrowers.aspx");
    }
}