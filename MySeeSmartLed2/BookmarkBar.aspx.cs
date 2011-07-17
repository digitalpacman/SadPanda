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

public partial class BookmarkBar : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		MembershipUser currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
		if (currentUser != null)
		{
			SaveBookmarkSection.Visible = true;
			LoginSection.Visible = false;
		}
		else
		{
			SaveBookmarkSection.Visible = false;
			LoginSection.Visible = true;
		}

		if (Request.QueryString["url"] != null)
		{
			HiddenCurrentUrl.Value = Server.HtmlEncode(Request.QueryString["url"].ToString());
		}
		else
		{
			HiddenCurrentUrl.Value = string.Empty;
		}
		TextBoxBookmarkTitle.Text = string.Empty;
	}

	private string GetCurrentUrl()
	{
		string currentUrl = string.Empty;
		string currentDomain = Request.ServerVariables["SERVER_NAME"].ToString();
		string currentPort = Request.ServerVariables["SERVER_PORT"].ToString();
		string currentPagePath = Request.ServerVariables["URL"].ToString();
		string currentQueryString = Request.ServerVariables["QUERY_STRING"].ToString();
		if (currentQueryString.Length > 0)
		{
			currentQueryString = "?" + currentQueryString;
		}
		if (currentPort.Length > 0)
		{
			currentPort = ":" + currentPort;
		}
		currentUrl = string.Format("http://{0}{1}{2}{3}", currentDomain, currentPort, currentPagePath, currentQueryString);
		return currentUrl;
	}

	protected void ButtonLogin_Click(object sender, EventArgs e)
	{
		string emailAddress = TextBoxEmailAddress.Text;
		string password = TextBoxPassword.Text;
		List<string> errors = UserManager.Login(emailAddress, password);
		if (errors.Count() > 0)
		{
			TextErrorMessage.InnerHtml = "(incorrect username/password)";
			TextErrorMessage.Visible = true;
		}
		else
		{
			Response.Redirect("BookmarkBar.aspx");
		}
	}
	protected void ButtonSaveBookmark_Click(object sender, EventArgs e)
	{
		if (IsPostBack)
		{
			MembershipUser currentUser = UserManager.GetCurrentUser();
			if (currentUser != null)
			{
				int userId = (int)currentUser.ProviderUserKey;
				string title = TextBoxBookmarkTitle.Text;
				string url = HiddenCurrentUrl.Value;
				if (url.Length > 0)
				{
					BookmarkManager.Add(userId, title, string.Empty, url);
				}
			}
		}
	}
}
