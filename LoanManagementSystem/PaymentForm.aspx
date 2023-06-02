<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentForm.aspx.cs" Inherits="PaymentForm" %>

<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Form</title>
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/customers.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/applicationform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/customerform.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-wrapper">
            <div class="sidebar-wrapper">
                <uc:Sidebar ID="Sidebar1" runat="server" />

            </div>
            <div class="content-parent-wrapper">
                <div class="navbar-wrapper">
                    <uc:Navbar ID="Navbar1" runat="server" Title="Payment Form" />
                </div>
                <div class="content-wrapper">
                    <div class="content-sub-wrapper">

                        <div class="loan-application-wrapper">

                            <div class="customer-information-wrapper">
                                <h2>Customer Information</h2>
                                <div class="customer-information-container">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblCustomerID" runat="server" Text="Customer ID"></asp:Label>
                                        <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                        <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                <h2>Loan Details</h2>
                                <div class="customer-information-container">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblLoanID" runat="server" Text="Loan ID"></asp:Label>
                                        <asp:TextBox ID="txtLoanID" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblLoanAmount" runat="server" Text="Amount"></asp:Label>
                                        <asp:TextBox ID="txtLoanAmount" runat="server" placeholder="Php 12345.00" type="number"></asp:TextBox>
                                    </div>
                                    <div class="app-input-group position-group">
                                        <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date"></asp:Label>
                                        <asp:TextBox ID="txtPaymentDate" runat="server" type="date" Style="width: 50%;">    </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                <div class="submit-btn-container">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Add Payment" />
                                    <div class="back-container">
                                        <a href="/Payments">Cancel
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
