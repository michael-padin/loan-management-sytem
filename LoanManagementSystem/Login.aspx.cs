using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fonts_Login : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-J4A1LCO\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;User ID=user;Password=user";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool ValidateLogin(string email, string password)
    {
        bool isValid = false;

        // Create a connection to the SQL Server database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Prepare the SQL query to validate the login credentials
            string query = "SELECT COUNT(*) FROM [Users] WHERE Email = @Email AND Password = @Password";

            // Create a command with the query and connection
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Set the parameters for the email and password
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                // Open the database connection
                connection.Open();

                // Execute the query and get the result
                int count = (int)command.ExecuteScalar();

                // Check if a matching record was found
                isValid = (count > 0);

                // Close the database connection
                connection.Close();
            }
        }

        return isValid;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text; // Assuming you have an email input field with ID "txtEmail"
        string password = txtPassword.Text; // Assuming you have a password input field with ID "txtPassword"

        // Call the ValidateLogin method to check the credentials
        bool isValidLogin = ValidateLogin(email, password);

        if (isValidLogin)
        {
            // Redirect the user to the home page or another authenticated page
            Response.Redirect("Borrowers.aspx");
        }
        else
        {
            // Display an error message to the user     
            lblGeneralErr.Text = "Invalid email or password.";
        }
    }
}