<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

	<form id="formLogin" runat="server" action="Login.aspx" method="post">

<asp:Repeater ID="MessageList" runat="server">
	<HeaderTemplate>
		<ul>
	</HeaderTemplate>
	<ItemTemplate>
		<li><%# Container.DataItem %></li>
	</ItemTemplate>
	<FooterTemplate>
		</ul>
	</FooterTemplate>
</asp:Repeater>


<asp:TextBox ID="TextBoxEmailAddress" runat="server"></asp:TextBox>
<asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
<asp:Button ID="ButtonLogin" Text="Login" runat="server" 
	onclick="ButtonLogin_Click" />


new user?

<asp:TextBox ID="TextBoxNewEmailAddress" runat="server"></asp:TextBox>
<asp:TextBox ID="TextBoxNewPassword" TextMode="Password" runat="server"></asp:TextBox>
<asp:TextBox ID="TextBoxNewPasswordConfirm" TextMode="Password" runat="server"></asp:TextBox>
<asp:Button ID="ButtonNewUser" Text="Join" runat="server" 
	onclick="ButtonNewUser_Click" />

</form>

</asp:Content>

