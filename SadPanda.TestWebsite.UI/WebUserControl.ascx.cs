using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

public partial class WebUserControl : System.Web.UI.UserControl
{
	public System.Drawing.Color BackColor { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
		myPanel.BackColor = BackColor;
    }

	protected override void Render(HtmlTextWriter writer)
	{
		base.Render(writer);
	}
}
