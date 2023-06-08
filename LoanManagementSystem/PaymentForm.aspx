<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentForm.aspx.cs" Inherits="PaymentForm" %>

<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Form</title>
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowers.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/applicationform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowerform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowerdetails.css" type="text/css" />
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
                                <h2>Payment Information</h2>
                                <div class="customer-information-container" style="flex-direction: column;">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblLoanID" runat="server" Text="Loan ID"></asp:Label>
                                        <asp:TextBox ID="txtLoanID" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblLoanIDErr" runat="server" Text=""></asp:Label>
                                    </div>
                                         <div class="app-input-group">
                                        <asp:Label ID="lblBorrowerID" runat="server" Text="Borrower ID"></asp:Label>
                                        <asp:TextBox ID="txtBorrowerID" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblBorrowerIDErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblPaymentID" runat="server" Text="Payment ID"></asp:Label>
                                        <asp:TextBox ID="txtPaymentID" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblPaymentIDErr" runat="server" Text=""></asp:Label>
                                    </div>
                                 
                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                <div class="customer-information-container"  style="flex-direction: column!important;"  >
                                    <div class="app-input-group">
                                        <asp:Label ID="lblPaymentAmount" runat="server" Text="Amount"></asp:Label>
                                        <asp:TextBox ID="txtPaymentAmount" runat="server" placeholder="Php 12345.00" type="number"></asp:TextBox>
                                        <asp:Label ID="lblPaymentAmountErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group ">
                                        <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date"></asp:Label>
                                        <asp:TextBox ID="txtPaymentDate" runat="server" type="date">    </asp:TextBox>
                                        <asp:Label ID="lblPaymentDateErr" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                <asp:Label ID="lblGeneralErr" runat="server" Text=""></asp:Label>
                                <div class="submit-btn-container">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Add Payment" OnClick="btnSubmt_Click" />
                                    <div class="back-container">
                                        <a href="/Loans">Cancel
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
