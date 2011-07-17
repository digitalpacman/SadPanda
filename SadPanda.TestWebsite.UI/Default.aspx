<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Namespace="SadPanda.UI.Controls" TagPrefix="sp" %>
<%@ Register src="WebUserControl.ascx" tagname="WebUserControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    	<sp:Class1 ID="Class1" runat="server"></sp:Class1>

    

    
    	<uc1:WebUserControl ID="WebUserControl1" runat="server" BackColor="#FF0000" />
    	
    	
    	
    	
    	<sp:Class1 runat="server"></sp:Class1>

</asp:Content>

