using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Payments : System.Web.UI.Page
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

    private void BindGridView()
    {
        // Create a new DataTable
        SqlConnection conn = new SqlConnection(connectionString);

        string query = "SELECT Payments.*, Borrowers.FullName FROM Payments INNER JOIN Borrowers ON Payments.BorrowerID = Borrowers.BorrowerID";

        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        SqlDataReader read = cmd.ExecuteReader();

        GridView1.DataSource = read;
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string paymentId = (e.CommandArgument).ToString();
        if (e.CommandName == "Delete")
        {
            string query = "DELETE FROM Payments WHERE PaymentID = @paymentId";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@paymentId", paymentId);

            con.Open();
            cmd.ExecuteNonQuery();
            BindGridView(); // Refresh GridView
        }
        else if (e.CommandName == "Update")
        {
            string query = "SELECT * FROM Payments WHERE PaymentID = @paymentId";

            // Perform update operation
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@paymentId", paymentId);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Response.Redirect("PaymentForm.aspx?loanId=" + reader["LoanID"].ToString());
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

        string query = "SELECT *  FROM Payments INNER JOIN Borrowers ON Payments.BorrowerID = Borrowers.BorrowerID WHERE Borrowers.FullName LIKE @searchString";
        SqlConnection conn = new SqlConnection(connectionString);


        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");
        conn.Open();

        SqlDataReader read = cmd.ExecuteReader();



        GridView1.DataSource = read;
        GridView1.DataBind();


        conn.Close();

    }
}