using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for UserManager
/// </summary>
public static class UserManager
{
	public static List<string> Login(string emailAddress, string password)
	{
		return Login(emailAddress, password, false);
	}

	public static List<string> Login(string emailAddress, string password, bool redirectAfterLogin)
	{
		emailAddress = emailAddress.Trim();
		password = password.Trim();
		List<string> errors = IsValidCredentials(emailAddress, password);
		if (errors.Count() == 0)
		{
			if (Membership.ValidateUser(emailAddress, password))
			{
				if (redirectAfterLogin)
				{
					FormsAuthentication.RedirectFromLoginPage(emailAddress, true);
				}
				else
				{
					FormsAuthentication.SetAuthCookie(emailAddress, true);
				}
			}
			else
			{
				errors.Add("Incorrect username and password combination.");
			}
		}
		return errors;
	}

	private static List<string> IsValidCredentials(string emailAddress, string password)
	{
		List<string> errors = new List<string>();

		if (emailAddress.Length == 0)
		{
			errors.Add("Email address is a required field.");
		}
		if (password.Length == 0)
		{
			errors.Add("Password is a required field.");
		}
		return errors;
	}

	public static MembershipUser GetCurrentUser()
	{
		return Membership.GetUser(HttpContext.Current.User.Identity.Name);
	}
}
