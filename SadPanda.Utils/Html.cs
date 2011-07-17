using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SadPanda.Utils.Html
{
	public static class HtmlFormatter
	{
		public static string FormatHtmlLinks(string html, bool openNewWindow)
		{
			string pattern = @"(http?://[^\s]+)";
			string replacement = string.Empty;
			if (openNewWindow)
			{
				replacement = @"<a href=""$1"" target=""_blank"">$1</a>";
			}
			else
			{
				replacement = @"<a href=""$1"">$1</a>";
			}
			Regex linkLocator = new Regex(pattern);
			return linkLocator.Replace(html, replacement);
		}

		private static void test()
		{
			int[] x = new int[] { 1, 2, 3, 4 };

			IEnumerable<int> b = x.Select(i => { return i; });
			var c = from i in x select new { i };

			Console.WriteLine("done");
		}

		struct mstruct : minterface
		{
			public void b()
			{
			}
			public int c { get; set; }
		}

		interface minterface
		{
			void b();
			int c { get; set; }
		}

		abstract class mabstract
		{
			public virtual void b()
			{

			}
			public abstract int c { get; set; }
		}

		class mclass2 : mabstract
		{
			public override void b()
			{

			}
			public override int c { get; set; }
		}

		public static class staticclass
		{

		}
	}
}
