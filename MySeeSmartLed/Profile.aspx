<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="_Profile" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<form Runat="server">
		<asp:TextBox ID="TextBoxOldPassword" Runat="server"></asp:TextBox>
		<asp:TextBox ID="TextBoxNewPassword" Runat="server"></asp:TextBox>
		<asp:TextBox ID="TextBoxConfirmNewPassword" Runat="server"></asp:TextBox>
		<asp:Button ID="ButtonChangePassword" Text="Save" runat="server" />

		<hr />
		
		<div>Edit Bookmarks:</div>
		
		<asp:Repeater ID="BookmarksRepeater" runat="server">
			<HeaderTemplate>
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
				<li><%# DataBinder.Eval(Container.DataItem, "Url") %></li>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
		</asp:Repeater>
		
	</form>
</asp:Content>