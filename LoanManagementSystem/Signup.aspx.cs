using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signup : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-TDH7QKT\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;Persist Security Info=True;User ID=user;Password=user";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        string name = txtName.Text;
        string password = txtPassword.Text;
        bool hasErr = false;

        if (string.IsNullOrEmpty(email)){
            lblEmailErr.Text = "Email cannot be empty";
            hasErr = true;
        } else
        {
            lblEmailErr.Text = "";
        }

        if (string.IsNullOrEmpty(name)){
            lblNameErr.Text = "Name cannot be empty";
            hasErr = true;
        } else
        {
            lblNameErr.Text = "";
        }

        if (string.IsNullOrEmpty(password)){
            lblPasswordErr.Text = "Password cannot be empty";
            hasErr = true;
        } else if (password.Length < 8)
        {
            lblPasswordErr.Text = "Password must be at least 8 characters long.";
            hasErr = true;
        } else
        {
            lblPasswordErr.Text = "";
        }

        if (hasErr) return;


        SqlConnection conn = new SqlConnection(connectionString);

        string checkUserQuery = "SELECT * FROM Users WHERE Email = @email";

        SqlCommand cmd = new SqlCommand(checkUserQuery, conn);
        cmd.Parameters.AddWithValue("@email", email);
        conn.Open();

        SqlDataReader reader = cmd.ExecuteReader(); 

        if (reader.Read())
        {
            lblEmailErr.Text = "Email already exist!";
            return;
        }


        string query = "INSERT INTO Users (Name, Email, Password) VALUES (@name, @email, @password)";

        SqlCommand cmd1 = new SqlCommand(query, conn);
        cmd1.Parameters.AddWithValue("@name", name);
        cmd1.Parameters.AddWithValue("@email", email);
        cmd1.Parameters.AddWithValue("@password", password);

        cmd1.ExecuteNonQuery();

        conn.Close();

        Response.Redirect("Login.aspx");
    }
}