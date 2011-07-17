using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel.Syndication;
using SadPanda.Utils.Html;

namespace SadPanda.Data.IO.Net
{
	public class RssHelper
	{
		public static string GetSingleRssFeed(string url, bool linkToArticle)
		{
			string content = string.Empty;
			XmlReader stream = XmlReader.Create(url);
			SyndicationFeed feed;
			feed = SyndicationFeed.Load(stream);
			if (feed.Items.Count() > 0)
			{
				SyndicationItem firstItem = feed.Items.First();
				content = HtmlFormatter.FormatHtmlLinks(firstItem.Summary.Text, true);
				if (linkToArticle && firstItem.Links.Count > 0)
				{
					content += string.Format(@" <a href=""{0}"" target=""_blank"">read full article</a>", firstItem.Links[0].Uri);
				}
			}
			return content;
		}
	}
}
