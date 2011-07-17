using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	public void GridViewNews_RowUpdating(Object sender, GridViewUpdateEventArgs e)
	{
		int index = GridViewNews.EditIndex;
		GridViewRow row = GridViewNews.Rows[index];
		TextBox description = (TextBox)row.FindControl("TextBoxDescription");
		e.NewValues["description"] = description.Text;
	}
}
