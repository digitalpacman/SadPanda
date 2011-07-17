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
using MySql.Data.MySqlClient;
using SadPanda.Data.Sql;

public partial class _Profile : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		MySqlDataReader bookmarkReader = SqlHelper.ExecuteReader("SELECT ");
		BookmarksRepeater.DataSource = bookmarkReader;
		BookmarksRepeater.DataBind();
	}
}
