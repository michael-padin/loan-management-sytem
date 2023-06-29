<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Navbar.ascx.cs" Inherits="Navbar" %>


<div class="navbar-container">
    <h1 class="navbar-title">
        <asp:Literal ID="LiteralTitle" runat="server"></asp:Literal>
        <%# Title %></h1>
    <div class="navbar-right">
        <div class="circle"><span>M</span> </div>
        <div class="navbar-right-info">
            <div style ="display: flex;">
                <h1>Hi &nbsp</h1>
            <asp:Label ID="lblFullName" runat="server" Text="" Style="font-size: 2rem;"></asp:Label>

            </div>
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </div>
    </div>
</div>
