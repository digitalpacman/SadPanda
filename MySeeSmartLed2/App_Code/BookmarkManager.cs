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
using SadPanda.Data.Sql;

/// <summary>
/// Summary description for BookmarkManager
/// </summary>
public static class BookmarkManager
{
	public static void Add(int memberId, string title, string description, string url)
	{
		SqlHelper.ExecuteNonQuery("INSERT INTO Bookmarks (MemberId, Title, Description, Url, CreateDate) VALUES ({0}, '{1}', '{2}', '{3}', CURRENT_TIMESTAMP)",
			memberId, title, description, url);
	}

	public static void Edit(int bookmarkId, int memberId, string title, string description, string url)
	{
		SqlHelper.ExecuteNonQuery("UPDATE Bookmarks SET Title = '{0}', Description = '{1}', Url = '{2}' WHERE BookmarkId = {3} AND MemberId = {4}",
			title, description, url, bookmarkId, memberId);
	}

	public static void Delete(int bookmarkId, int memberId)
	{
		SqlHelper.ExecuteNonQuery("DELETE FROM Bookmarks WHERE BookmarkId = {0} AND MemberId = {1}",
			bookmarkId, memberId);
	}
}
