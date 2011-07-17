<%@ Page Language="C#" AutoEventWireup="true" Inherits="EditNews" Codebehind="EditNews.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		&nbsp;<asp:GridView ID="GridViewNews" runat="server" AutoGenerateColumns="False" 
			DataKeyNames="NewsId" DataSourceID="MySqlDataSource" AutoGenerateEditButton="true" onrowupdating="GridViewNews_RowUpdating">
			<Columns>
				<asp:BoundField DataField="NewsId" HeaderText="NewsId" InsertVisible="False" 
					ReadOnly="True" SortExpression="NewsId" />
				<asp:TemplateField HeaderText="Description" SortExpression="Description">
					<EditItemTemplate>
						<asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
					</EditItemTemplate>
					<ItemTemplate>
						<%#Eval("Description") %>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		
		</asp:GridView>
    	<asp:SqlDataSource ID="MySqlDataSource" runat="server" 
			ConnectionString="<%$ ConnectionStrings:SqlServices %>" 
			ProviderName="<%$ ConnectionStrings:SqlServices.ProviderName %>" 
			SelectCommand="SELECT NewsId, Description FROM News"
			UpdateCommand="UPDATE News SET Description = @Description WHERE NewsID = @NewsID">
			<UpdateParameters>
				<asp:Parameter Name="Description" Type="String" />
			</UpdateParameters>
		</asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
