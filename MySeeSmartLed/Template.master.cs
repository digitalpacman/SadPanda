using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Text.RegularExpressions;
using SadPanda.Utils.Html;
using SadPanda.Data.IO.Net;
using SadPanda.Data.Sql;
using MySql.Data.MySqlClient;

public partial class Template : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		string customerType = string.Empty;
		customerType = "user";
		List<Bookmark> newDataSource = new List<Bookmark>();
		switch (customerType)
		{
			case "user":
				newDataSource.Add(new Bookmark("A", "http://www.google.com"));
				newDataSource.Add(new Bookmark("B", "http://www.google.com"));
				break;
			case "sales":
				break;
			case "distributor":
				break;
			case "admin":
				break;
		}

		repeaterSideBarLinks.DataSource = newDataSource;
		repeaterSideBarLinks.DataBind();

		liTwitterContent.InnerHtml = RssHelper.GetSingleRssFeed("http://twitter.com/statuses/user_timeline/55351109.rss", false);
		liBlogContent.InnerHtml = RssHelper.GetSingleRssFeed("http://killthewatts.seesmartled.com/?feed=rss2", true);

		MySqlDataReader news = SqlHelper.ExecuteReader("SELECT Description FROM News LIMIT 1");
			divNews.InnerHtml = news["Description"].ToString();

		SqlHelper.ExecuteReader("SELECT TITLE FROM Bookmarks WHERE MemberId = '{0}'", "1");
		MembershipUser currentUser = UserManager.GetCurrentUser();
		if (currentUser != null)
		{
			MySqlDataReader bookmarks = SqlHelper.ExecuteReader("SELECT Title, Description, Url, CreateDate FROM Bookmarks WHERE MemberId = {0}"
				, currentUser.ProviderUserKey);
			if (bookmarks.HasRows)
			{
				repeaterBookmarks.DataSource = bookmarks;
				repeaterBookmarks.DataBind();
			}
		}
	}

	private class Bookmark
	{
		public string Text { get; set; }
		public string Url { get; set; }

		public Bookmark(string text, string url)
		{
			this.Text = text;
			this.Url = url;
		}
	}

	private static string CreateNavigationLink(string text, string url)
	{
		return string.Format(@"<li><a href=""{0}"">{1}</a></li>", url, text);
	}
}
