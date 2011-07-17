using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using SadPanda.Data.Sql;
using MySql.Data.MySqlClient;
using MySql.Web.Security;

public partial class Login : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Request.QueryString["SignOut"] != null)
		{
			FormsAuthentication.SignOut();
		}
	}

	private bool LoginAs(string userName, string password)
	{
		if (Membership.ValidateUser(userName, password))
		{
			FormsAuthentication.RedirectFromLoginPage(userName, true);
			return true;
		}
		return false;
	}
	protected void ButtonLogin_Click(object sender, EventArgs e)
	{
		if (IsPostBack)
		{
			string emailAddress = TextBoxEmailAddress.Text;
			string password = TextBoxPassword.Text;

			List<string> messages = UserManager.Login(emailAddress, password, true);
			
			MessageList.DataSource = messages;
			MessageList.DataBind();
		}
	}
	protected void ButtonNewUser_Click(object sender, EventArgs e)
	{
		if (IsPostBack)
		{
			string emailAddress = TextBoxNewEmailAddress.Text.Trim();
			string password = TextBoxNewPassword.Text.Trim();
			string passwordConfirm = TextBoxNewPasswordConfirm.Text.Trim();

			List<string> messages = new List<string>();

			if (password != passwordConfirm)
			{
				messages.Add("The password you entered doesn't match the confirm password field.");
			}
			else
			{
				Membership.CreateUser(emailAddress, password, emailAddress);
				UserManager.Login(emailAddress, password, true);
			}
			MessageList.DataSource = messages;
			MessageList.DataBind();
		}
	}
}
