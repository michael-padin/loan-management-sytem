<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BorrowerDetails.aspx.cs" Inherits="CustomerDetails" %>

<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowers.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/applicationform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowerform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowerdetails.css" type="text/css" />
    <title>Customer Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-wrapper">
            <div class="sidebar-wrapper">
                <uc:Sidebar ID="Sidebar1" runat="server" />

            </div>
            <div class="content-parent-wrapper">
                <div class="navbar-wrapper">
                    <uc:Navbar ID="Navbar1" runat="server" Title="Customer Details" />
                </div>
                <div class="content-wrapper">
                    <div class="content-sub-wrapper">

                        <div class="loan-application-wrapper">
                            <div class="customer-information-container">
                                <h2>Borrower Information</h2>
                                <div class="customer-information-container">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblBorrowerID" runat="server" Text="Borrower ID"></asp:Label>
                                        <asp:TextBox ID="txtBorrowerID" runat="server" required="true" ></asp:TextBox>
                                        <span style="font-size: .75rem; margin-top: 10px; margin-left: 5px;">You can enter unique number to identify the borrower such as Social Security Number, License #, Registration Id</span>
                                        <asp:Label ID="lblBorrowerIDErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblCustomerName" runat="server" Text="Full Name"></asp:Label>
                                        <asp:TextBox ID="txtCustomerName" runat="server" required="true" placeholder="First Last"></asp:TextBox>
                                        <asp:Label ID="lblCustomerNameErr" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div style="display: flex; width: 100%; gap: 1rem;"> 
                                        <div class="app-input-group">
                                            <asp:Label ID="lblContact" runat="server" Text="Mobile #"></asp:Label>
                                            <asp:TextBox ID="txtContact" runat="server" type="tel" placeholder="0987654321" required="true"></asp:TextBox>
                                            <asp:Label ID="lblContactErr" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="app-input-group email-group">
                                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                                            <asp:TextBox ID="txtEmail" runat="server" type="email" placeholder="name@example.com" required="true"></asp:TextBox>
                                            <asp:Label ID="lblEmailErr" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="app-input-group address-group">
                                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                        <asp:TextBox ID="txtAddress" runat="server" type="text" placeholder="Enter your address." required="true"></asp:TextBox>
                                        <asp:Label ID="lblAddressErr" runat="server" Text=""></asp:Label>
                                    </div>

                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                <h2>Employement Details</h2>
                                <div class="customer-information-container">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblEmployerName" runat="server" Text="Employer"></asp:Label>
                                        <asp:TextBox ID="txtEmployerName" runat="server" required="true"></asp:TextBox>
                                          <asp:Label ID="lblEmployerNameErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblMonthlyIncome" runat="server" Text="Monthly Income"></asp:Label>
                                        <asp:TextBox ID="txtMonthlyIncome" runat="server" required="true" type="number" placeholder="30000"></asp:TextBox>
                                        <asp:Label ID="lblMonthlyIncomeErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group position-group">
                                        <asp:Label ID="lblPosition" runat="server" Text="Occupation/Possition"></asp:Label>
                                        <asp:TextBox ID="txtPosition" runat="server" required="true" placeholder="Enter your position"></asp:TextBox>
                                          <asp:Label ID="lblPositionErr" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                  <asp:Label ID="lblGeneralErr" runat="server" Text=""></asp:Label>
                                <div class="submit-btn-container">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                    <div class="back-container">
                                        <a href="/Borrowers">Cancel
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
