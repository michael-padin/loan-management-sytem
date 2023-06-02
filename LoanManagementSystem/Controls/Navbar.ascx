<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Navbar.ascx.cs" Inherits="Navbar" %>


<div class="navbar-container">
    <h1 class="navbar-title">
        <asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>
        <%# Title %></h1>
    <div class="navbar-right">
        <div class="circle"><span>M</span> </div>
        <div class="navbar-right-info">
            <h2>Michael Padin</h2>
            <span>ID</span> 78945247
        </div>
    </div>
</div>
