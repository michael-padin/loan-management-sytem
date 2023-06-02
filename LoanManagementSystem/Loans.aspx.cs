using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Loans : System.Web.UI.Page
{

    string connectionString = "Data Source=DESKTOP-TDH7QKT\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;Persist Security Info=True;User ID=user;Password=user";
    protected void Page_Load(object sender, EventArgs e)
    {


        btnSearch.ServerClick += BtnSearch_Click;
        btnClearSearch.ServerClick += btnClearSearch_Click;

        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        // Create a new DataTable
        SqlConnection conn = new SqlConnection(connectionString);

        string query = "SELECT Loans.*, Borrowers.FullName FROM Loans INNER JOIN Borrowers ON Loans.BorrowerID = Borrowers.BorrowerID";

        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        SqlDataReader read = cmd.ExecuteReader();

        GridView1.DataSource = read;
        GridView1.DataBind();
    }


    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Text = string.Empty;
        BindGridView();
    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        // Handle the button click event
        // Perform the desired action
        string searchString = txtSearch.Text;

        string query = "SELECT *  FROM Loans INNER JOIN Borrowers ON Loans.BorrowerID = Borrowers.BorrowerID WHERE Borrowers.FullName LIKE @searchString";
        SqlConnection conn = new SqlConnection(connectionString);


        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");
        conn.Open();

        SqlDataReader read = cmd.ExecuteReader();



        GridView1.DataSource = read;
        GridView1.DataBind();


        conn.Close();

    }

    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string loanId = (e.CommandArgument).ToString();
        if (e.CommandName == "Delete")
        {
            string query = "DELETE FROM Loans WHERE LoanID = @loanId";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@loanId", loanId);

            con.Open();
            cmd.ExecuteNonQuery();
            BindGridView(); // Refresh GridView
        }
        else if (e.CommandName == "Update")
        {
            string query = "SELECT * FROM Loans WHERE LoanID = @loanId";

            // Perform update operation
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@loanId", loanId);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Response.Redirect("EditLoan.aspx?loanId="+ loanId);
            }
            else
            {
                return;
            }
        }


    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Refresh GridView
        BindGridView();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Refresh GridView

    }
}


