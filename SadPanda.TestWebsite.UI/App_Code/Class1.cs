using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace SadPanda.UI.Controls
{
	/// <summary>
	/// Summary description for Class1
	/// </summary>
	public class Class1 : System.Web.UI.WebControls.TextBox
	{
		protected override void Render(HtmlTextWriter output)
		{
			output.Write("WEEE");
		}

	}
}