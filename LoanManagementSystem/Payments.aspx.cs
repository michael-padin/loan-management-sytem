using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Payments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView(); 
            
        }
    }

    private void BindGridView()
    {
        List<Payment> payments = GetStaticPaymentTestData();
        GridView1.DataSource = payments;
        GridView1.DataBind();
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int LoanId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }

    private List<Payment> GetStaticPaymentTestData()
    {
        List<Payment> payments = new List<Payment>();

        // Generate static test data for payments
        payments.Add(new Payment { PaymentId = 1, CustomerId = 1, CustomerName = "John Doe", LoanId = 1, PaymentDate = new DateTime(2022, 2, 15), Amount = 1000 });
        payments.Add(new Payment { PaymentId = 2, CustomerId = 1, CustomerName = "John Doe", LoanId = 1, PaymentDate = new DateTime(2022, 3, 10), Amount = 500 });
        payments.Add(new Payment { PaymentId = 3, CustomerId = 2, CustomerName = "Jane Smith", LoanId = 2, PaymentDate = new DateTime(2022, 4, 5), Amount = 1000 });
        payments.Add(new Payment { PaymentId = 4, CustomerId = 3, CustomerName = "Robert Johnson", LoanId = 3, PaymentDate = new DateTime(2022, 5, 20), Amount = 800 });

        return payments;
    }
}