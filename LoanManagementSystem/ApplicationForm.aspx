<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationForm.aspx.cs" Inherits="ApplicationForm" %>

<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application Form</title>
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/borrowers.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/applicationform.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-wrapper">
            <div class="sidebar-wrapper">
                <uc:Sidebar ID="Sidebar1" runat="server" />

            </div>
            <div class="content-parent-wrapper">
                <div class="navbar-wrapper">
                    <uc:Navbar ID="Navbar1" runat="server" Title="Application Form" />
                </div>
                <div class="content-wrapper">
                    <div class="content-sub-wrapper">
                        <div class="loan-application-wrapper">
                            <div class="customer-information-wrapper">
                                <h2>Loan Information</h2>

                                <div class="customer-information-wrapper">
                                    <div class="borrower-ddl-container">
                                        <asp:Label ID="lblBorrower" runat="server" Text="Borrower"></asp:Label>
                                        <asp:DropDownList ID="ddlBorrowers" runat="server"></asp:DropDownList>
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
                                        <asp:Label ID="lblTotalLoanAmntErr" runat="server" Text="" ></asp:Label>
                                    </div>
                                    <div class="app-input-group">
                                        <asp:Label ID="lblPaymentPerMonth" runat="server" Text="Payment per month"></asp:Label>
                                        <asp:TextBox ID="txtPaymentPerMonth" runat="server" ></asp:TextBox>
                                        <asp:Label ID="lblPaymentPerMonthErr" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="customer-information-container">



                                <div class="app-input-group">
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
                                    <asp:TextBox ID="txtStartDate" runat="server" type="date" onkeyup="calculateEndDate()" onchange ="calculateEndDate()"></asp:TextBox>
                                    <asp:Label ID="lblStartDateErr" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="app-input-group">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
                                    <asp:TextBox ID="txtEndDate" runat="server" type="date"  ></asp:TextBox>
                                    <asp:Label ID="lblEndDateErr" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="app-input-group" style="flex: initial">
                                    <asp:Label ID="lblNumOfRepayments" runat="server" Text="Number of Repayments (in months)"></asp:Label>
                                    <asp:TextBox ID="txtNumOfRepayments" runat="server" type="number"></asp:TextBox>
                                    <asp:Label ID="lblNumOfRepaymentsErr" runat="server" Text=""></asp:Label>
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
                                <div class="confirmation-wrapper">
                                    <p>
                                        By checking the box below, I acknowledge and agree to the terms and conditions outlined in the loan agreement.
                                    </p>

                                    <div style="display: flex; flex-direction: column;">
                                        <div class="confirmation-container">
                                            <asp:CheckBox ID="termsCheckbox" runat="server" />
                                            <p>agree to the terms and conditions</p>
                                        </div>
                                        <asp:Label ID="termsCheckboxErr" runat="server" Text=""></asp:Label>
                                    </div>

                                    <p>
                                        Please check the box to indicate your agreement with the terms and conditions of the loan. By doing so, you confirm that you have read and understood the terms and conditions and agree to be bound by them.
                                    </p>

                                    <p>
                                        Please note that this is a simplified example, and the actual terms and conditions may vary depending on the specific loan agreement and lender. Make sure to provide the complete and accurate terms and conditions for the applicant to review before checking the agreement checkbox.
                                    </p>
                                </div>
                            </div>
                            <div class="customer-information-wrapper">
                                <asp:Label ID="lblgeneralErr" runat="server" Text=""></asp:Label>
                                <div class="submit-btn-container">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClick="btnSubmit_Click" />
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

            // Set the result in the txtTotalLoanAmnt
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

