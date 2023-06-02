<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>
<%@ Register Src="~/Controls/Navbar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Controls/Sidebar.ascx" TagPrefix="uc" TagName="Sidebar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Content/Css/index.css" type="text/css" />
    <link rel="stylesheet" href="Content/Css/dashboard.css" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-wrapper">
            <div class="sidebar-wrapper">
               <uc:Sidebar ID="Sidebar1" runat="server" />

            </div>
            <div class="content-parent-wrapper">
                <div class="navbar-wrapper">
                  <uc:Navbar ID="Navbar1" runat="server" Title="Dashboard" />
                </div>
                  <div class="content-wrapper">
                    <div class="content-sub-wrapper">
                        <div class="content-container">
                            <div>Searchbar here</div>
                            <div></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
