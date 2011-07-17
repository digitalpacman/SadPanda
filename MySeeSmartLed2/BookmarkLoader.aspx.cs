using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class BookmarkLoader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		MembershipUser currentUser = Membership.GetUser(HttpContext.Current.User.Identity.Name);
		if (currentUser != null)
		{
			Response.Write("function ShowBookmarkBar() {");
			Response.Write(@"document.write('<div id=""BookmarkSection""><iframe src=""http://www.myseesmartled.com/BookmarkBar.aspx"" id=""BookmarkBar""></iframe></div>');");
			Response.Write("} ShowBookmarkBar();");
		}
    }
}
