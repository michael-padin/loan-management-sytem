<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditLoan.aspx.cs" Inherits="EditLoan" %>

<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowers.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/applicationform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowerdetails.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowerform.css" type="text/css" />

    <title>Edit Loans</title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-wrapper">
            <div class="sidebar-wrapper">
                <uc:Sidebar ID="EditLoanSidebar" runat="server" />

            </div>
            <div class="content-parent-wrapper">
                <div class="navbar-wrapper">
                    <uc:Navbar ID="EditLoanNavbar" runat="server" Title="Edit Loan" />
                </div>
                <div class="content-wrapper">
                    <div class="content-sub-wrapper">
                        <div class="loan-application-wrapper">
                            <div class="customer-information-wrapper">
                                <h2>Loan Information</h2>

                                <div class="customer-information-wrapper">
                                    <div class="borrower-ddl-container">
                                        <asp:Label ID="lblBorrower" runat="server" Text="Borrower"></asp:Label>
                                        <asp:TextBox ID="txtBorrower" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblBorrowerErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblLoanID" runat="server" Text="Loan ID"></asp:Label>
                                        <asp:TextBox ID="txtLoanID" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblLoanIDErr" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>

                            </div>


                            <div class="customer-information-container">
                                <div style="display: flex; width: 100%; gap: 1rem;">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblAmountRequested" runat="server" Text="Loan Amount Requested"></asp:Label>
                                        <asp:TextBox ID="txtAmountRequested" runat="server" onkeyup="calculateLoanAmount()"></asp:TextBox>
                                        <asp:Label ID="lblAmountRequestionErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblLoanTerm" runat="server" Text="Loan Duration(in years)"></asp:Label>
                                        <asp:TextBox ID="txtLoanTerm" runat="server" onkeyup="runCalculations()" onchange="runCalculations ()"></asp:TextBox>
                                        <asp:Label ID="lblLoanTermErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblInterestRate" runat="server" Text="Interest Rate"></asp:Label>
                                        <asp:TextBox ID="txtInterestRate" runat="server" type="number" onkeyup="runCalculations()"></asp:TextBox>
                                        <asp:Label ID="lblInterestRateErr" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div style="display: flex; width: 100%; gap: 1rem; flex-direction: column;">
                                    <div class="app-input-group">
                                        <asp:Label ID="lblTotalLoanAmnt" runat="server" Text="Total Loan Amount"></asp:Label>
                                        <asp:TextBox ID="txtTotalLoanAmnt" runat="server" onchange="calculatePaymentPerMonth()"></asp:TextBox>
                                        <asp:Label ID="lblTotalLoanAmntErr" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblPaymentPerMonth" runat="server" Text="Payment per month"></asp:Label>
                                        <asp:TextBox ID="txtPaymentPerMonth" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblPaymentPerMonthErr" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="customer-information-container">



                                <div class="app-input-group">
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
                                    <asp:TextBox ID="txtStartDate" runat="server" type="date" onkeyup="calculateEndDate()" onchange="calculateEndDate()"></asp:TextBox>
                                    <asp:Label ID="lblStartDateErr" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="app-input-group">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
                                    <asp:TextBox ID="txtEndDate" runat="server" type="date"></asp:TextBox>
                                    <asp:Label ID="lblEndDateErr" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="app-input-group" style="flex: initial">
                                    <asp:Label ID="lblNumOfRepayments" runat="server" Text="Number of Repayments (in months)"></asp:Label>
                                    <asp:TextBox ID="txtNumOfRepayments" runat="server" type="number"></asp:TextBox>
                                </div>
                            </div>

                            <div class="customer-information-container">
                                <div class="status-ddl-container">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Text="Open" Value="open"></asp:ListItem>
                                        <asp:ListItem Text="Paid" Value="paid"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="rejected"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="customer-information-wrapper">
                                <asp:Label ID="generalErr" runat="server" Text=""></asp:Label>
                                <div class="submit-btn-container">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Update" OnClick="btnUpdate_Click" />
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

    <script type="text/javascript">

        function runCalculations() {
            calculateLoanAmount();
            calculateNumberOfRepayments();
            calculatePaymentPerMonth();
            calculateEndDate();
        }

        function calculateLoanAmount() {
            // Get the input values from the user
            const principal = parseFloat(document.getElementById("txtAmountRequested").value);
            const interestRate = parseFloat(document.getElementById("txtInterestRate").value);
            const loanTermInYears = parseInt(document.getElementById("txtLoanTerm").value);

            // Convert loan term to months
            const loanTermInMonths = loanTermInYears * 12;

            // Calculate the total interest
            const totalInterest = (principal * interestRate * loanTermInMonths) / 100;

            // Calculate the total loan amount
            const totalLoanAmount = principal + totalInterest || principal || 0;

            // Set the result in the TotalLoanAmountTextBox
            document.getElementById("txtTotalLoanAmnt").value = totalLoanAmount.toFixed(2);
        }

        function calculateEndDate() {
            // Get the input values from the user
            const startDateString = document.getElementById("txtStartDate").value;
            const loanTermInYears = parseInt(document.getElementById("txtLoanTerm").value);

            // Parse the start date
            const startDate = new Date(startDateString);

            // Calculate the end date
            const endDate = new Date(startDate.getFullYear() + loanTermInYears, startDate.getMonth(), startDate.getDate());

            // Format the end date as a string
            const endDateString = endDate.toISOString().split('T')[0]; // Get the date part only

            // Set the result in the EndDateTextBox
            document.getElementById("txtEndDate").value = endDateString;
        }

        function calculateNumberOfRepayments() {
            // Get the input values from the user
            const loanTermInYears = parseInt(document.getElementById("txtLoanTerm").value);

            console.log(loanTermInYears)


            // Calculate the number of repayments (assuming monthly repayment frequency)
            const numberOfRepayments = parseInt(loanTermInYears * 12);

            console.log(numberOfRepayments)

            // Display the result
            document.getElementById("txtNumOfRepayments").value = numberOfRepayments || 0;
        }

        function calculatePaymentPerMonth() {
            const loanTerm = parseInt(document.getElementById("txtLoanTerm").value);
            const totalLoanAmount = parseFloat(document.getElementById("txtTotalLoanAmnt").value);

            const paymentPerMonth = totalLoanAmount / (loanTerm * 12);

            document.getElementById("txtPaymentPerMonth").value = paymentPerMonth.toFixed(2) || 0;
        }
    </script>
</body>
</html>
