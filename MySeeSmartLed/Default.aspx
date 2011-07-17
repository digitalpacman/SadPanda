<%@ Page Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Welcome <span id="spanName" runat="server"></span> <a href="">edit profile</a></h2>
    <p>welcome message</p>
    <h2>Recent Bookmarks</h2>
    <ul>
		<asp:Repeater ID="repeaterRecentBookmarks" runat="server">
			<ItemTemplate>
				<li><a href=""></a><br /></li>
			</ItemTemplate>
		</asp:Repeater>
    </ul>
    <h2>Recent Blog</h2>
    <p id="pRecentBlog" runat="server"></p>
</asp:Content>

