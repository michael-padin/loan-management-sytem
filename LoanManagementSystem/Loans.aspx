<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Loans.aspx.cs" Inherits="Loans" %>


<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowers.css" type="text/css" />
    <title>Loans</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-wrapper">
            <div class="sidebar-wrapper">
                <uc:Sidebar ID="Sidebar1" runat="server" />

            </div>
            <div class="content-parent-wrapper">
                <div class="navbar-wrapper">
                    <uc:Navbar ID="Navbar1" runat="server" Title="Loans" />
                </div>
                <div class="content-wrapper">
                    <div class="content-sub-wrapper">
                        <div class="customer-wrapper">
                            <div class="searchbar-wrapper">
                                 <div class="searchbar-container">
                                    <button id="btnSearch" runat="server">
                                        <span class="icon-container">
                                            <?xml version="1.0" encoding="utf-8" ?>
                                            <!-- Uploaded to: SVG Repo, www.svgrepo.com, Generator: SVG Repo Mixer Tools -->
                                            <svg fill="currentColor" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg">
                                                <title>alt-lens</title>
                                                <path d="M0 14.048q0 2.848 1.12 5.44t2.976 4.48 4.48 2.976 5.472 1.12q4.064 0 7.52-2.24l5.312 5.312q0.864 0.864 2.112 0.864t2.144-0.864 0.864-2.112-0.864-2.144l-5.312-5.312q2.24-3.456 2.24-7.52 0-2.88-1.12-5.472t-2.976-4.448-4.48-3.008-5.44-1.12q-2.88 0-5.472 1.12t-4.48 3.008-2.976 4.448-1.12 5.472zM4 14.048q0-2.752 1.344-5.056t3.648-3.648 5.056-1.344q2.688 0 5.024 1.344t3.648 3.648 1.344 5.056q0 2.72-1.344 5.024t-3.648 3.648-5.024 1.344q-2.752 0-5.056-1.344t-3.648-3.648-1.344-5.024z"></path>
                                            </svg>
                                        </span>
                                    </button>
                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Search here..."></asp:TextBox>
                                    <button id="btnClearSearch" runat="server">
                                        <span class="icon-container">
                                            <?xml version="1.0" encoding="utf-8" ?>
                                            <!-- Uploaded to: SVG Repo, www.svgrepo.com, Generator: SVG Repo Mixer Tools -->
                                            <svg  viewBox="0 0 24 24" fill="none"
                                                xmlns="http://www.w3.org/2000/svg">
                                                <path fill-rule="evenodd" clip-rule="evenodd" d="M19.207 6.207a1 1 0 0 0-1.414-1.414L12 10.586 6.207 4.793a1 1 0 0 0-1.414 1.414L10.586 12l-5.793 5.793a1 1 0 1 0 1.414 1.414L12 13.414l5.793 5.793a1 1 0 0 0 1.414-1.414L13.414 12l5.793-5.793z" fill="#000000" />
                                            </svg></span>
                                    </button>
                                </div>
                                 <div class="add-wrapper">
                                    <a href="/ApplicationForm" class="add-link">
                                        <span class="add-container">
                                            <?xml version="1.0" encoding="utf-8" ?>
                                            <!-- Uploaded to: SVG Repo, www.svgrepo.com, Generator: SVG Repo Mixer Tools -->
                                            <svg viewBox="0 0 20 20"
                                                xmlns="http://www.w3.org/2000/svg" fill="none">
                                                <path fill="currentcolor" fill-rule="evenodd" d="M9 17a1 1 0 102 0v-6h6a1 1 0 100-2h-6V3a1 1 0 10-2 0v6H3a1 1 0 000 2h6v6z" />
                                            </svg>
                                        </span>
                                    </a>
                                </div>
                            </div>
                            <div class="customers-container">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="customers-table"  OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:Button ID="btnPay" runat="server" Text="Pay" CssClass="btnPay" CommandName="Pay" CommandArgument='<%# Eval("LoanId") %>'  />
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btnEdit" CommandName="Update" CommandArgument='<%# Eval("LoanId") %>' />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnDelete" CommandName="Delete" CommandArgument='<%# Eval("LoanId") %>'  />
                                                <!-- Add more buttons as needed -->
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LoanId" HeaderText="Loan ID" />
                                        <asp:BoundField DataField="BorrowerID" HeaderText="Borrower ID" />
                                        <asp:BoundField DataField="FullName" HeaderText="Borrower" />
                                        <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" DataFormatString="₱{0:N2}" />
                                        <asp:BoundField DataField="InterestRate" HeaderText="Interest Rate"  DataFormatString="{0}%"/>
                                        <asp:BoundField DataField="TotalLoanAmount" HeaderText="Total Loan Amount" DataFormatString="₱{0:N2}"/>
                                        <asp:BoundField DataField="LoanTerm" HeaderText="Loan Term (years)" />
                                        <asp:BoundField DataField="MonthlyPayment" HeaderText="Monthly Payment" DataFormatString="₱{0:N2}"/>
                                        <asp:BoundField DataField="Paid" HeaderText="Paid" DataFormatString="₱{0:N2}"/>
                                        <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="₱{0:N2}"/>
                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="LastPaymentDate" HeaderText="Last Payment Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="NumOfRepayments" HeaderText="Number of Repayments" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function ShowAlert(message) {
            alert(message);
        }

    </script>
</body>
</html>

