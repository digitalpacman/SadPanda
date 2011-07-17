<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookmarkBar.aspx.cs" Inherits="BookmarkBar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Bookmark Bar</title>
</head>
<body>
    <form id="formNewBookmark" runat="server">
    <div runat="server" id="SaveBookmarkSection">
		Title: <asp:TextBox ID="TextBoxBookmarkTitle" runat="server"></asp:TextBox>
		<asp:HiddenField ID="HiddenCurrentUrl" runat="server" />
		<asp:Button ID="ButtonSaveBookmark" Text="Save Bookmark" runat="server" 
			onclick="ButtonSaveBookmark_Click" />
    </div>
    <div runat="server" id="LoginSection">
		Login to use bookmarks <span visible="false" runat="server" id="TextErrorMessage"></span> - 
		Username: <asp:TextBox ID="TextBoxEmailAddress" runat="server"></asp:TextBox>
		Password: <asp:TextBox ID="TextBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
		<asp:Button ID="ButtonLogin" Text="Login" runat="server" 
			onclick="ButtonLogin_Click" />
    </div>
    </form>
</body>
</html>
