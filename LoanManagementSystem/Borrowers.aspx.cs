using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users : System.Web.UI.Page
{
    string connectionString = "Data Source=DESKTOP-J4A1LCO\\SQLEXPRESS;Initial Catalog=LoanManagementSystem;User ID=user;Password=user";

    protected void Page_Load(object sender, EventArgs e)
    {

        btnSearch.ServerClick += BtnSearch_Click;
        btnClearSearch.ServerClick += btnClearSearch_Click;

        if (!IsPostBack)
        {
            BindGridView();
        }
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

        string query = "SELECT * FROM Borrowers WHERE FullName LIKE @searchString OR Email LIKE @searchString";
        SqlConnection conn = new SqlConnection(connectionString);


        SqlCommand cmd = new SqlCommand(query, conn);

        cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");
        conn.Open();

        SqlDataReader read = cmd.ExecuteReader();



        GridView1.DataSource = read;
        GridView1.DataBind();


        conn.Close();

    }


    protected void BindGridView()
    {
        // Create a new DataTable
        SqlConnection conn = new SqlConnection(connectionString);

        string query = "SELECT * FROM Borrowers";

        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();

        SqlDataReader read = cmd.ExecuteReader();

        GridView1.DataSource = read;
        GridView1.DataBind();
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = (e.CommandArgument).ToString();
        if (e.CommandName == "Delete")
        {
            string query = "DELETE FROM Borrowers WHERE BorrowerID = @Id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            BindGridView(); // Refresh GridView
        }
        else if (e.CommandName == "Update")
        {
            string query = "SELECT * FROM Borrowers WHERE BorrowerID = @Id";

            // Perform update operation
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string borrowerId = reader["BorrowerID"].ToString();
                string name = reader["FullName"].ToString();
                string address = reader["Address"].ToString();
                string email = reader["Email"].ToString();
                string mobile = reader["Mobile"].ToString();
                string employer = reader["Employer"].ToString();
                string monthlyIncome = reader["MonthlyIncome"].ToString();
                string occupation = reader["Occupation"].ToString();

                Response.Redirect("BorrowerDetails.aspx?borrowerId="
                    + borrowerId + "&name=" + name + "&address=" + address + "&email=" +
                    email + "&mobile=" + mobile + "&employer=" + employer + "&monthlyIncome=" + monthlyIncome
                    + "&occupation=" + occupation
                    );
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