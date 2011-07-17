using System;
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
using System.Net;
using System.IO;
using System.Text;
using HoN;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using HoNModel;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
		/*
		 * http://replays.heroesofnewerth.com/match_replay.php?mid=10956260
		 * player data for a match
		 * replays.heroesofnewerth.com/player_pop.php
			Content-Type: application/x-www-form-urlencoded; charset=UTF-8

			mid=10956260&aid=566479
		*/
		HoNEntities context;
		Games myGames;
		context = new HoNEntities();
		context.SaveChanges();
		context.Dispose();


		Chart1.Series.Add("kills");
		Chart1.Series.Add("deaths");
		Chart1.Series.Add("assists");

		Chart1.Series["kills"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
		Chart1.Series["deaths"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
		Chart1.Series["assists"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;

		Chart1.Series["kills"].Points.AddXY(1.0, 1.0);
		Chart1.Series["kills"].Points.AddXY(2.0, 2.0);
		Chart1.Series["deaths"].Points.AddXY(3.0, 3.0);
		Chart1.Series["deaths"].Points.AddXY(1.2, 1.0);
		Chart1.Series["assists"].Points.AddXY(1.5, 2.0);

		Chart1.Legends.Add("Heros");
		return;

		Game curGame = new Game();
		curGame.Id = "10956260";
		string matchText = HttpRequest("http://replays.heroesofnewerth.com/match_replay.php?mid=" + curGame.Id.ToString());
		//string matchText = HttpRequest("http://localhost:4655/HoN/match.html?mid=" + curGame.Id.ToString());

		MatchCollection playerNames = Regex.Matches(matchText, @"id=[""]player\d+[""]>([^<]+)[\s\S]+?getMatchStats\(\d+\,\s*(\d+)");
		foreach (Match playerName in playerNames)
		{
			string heroText = HttpRequest("POST", "http://replays.heroesofnewerth.com/player_pop.php", "mid=" + curGame.Id.ToString() + "&aid=" + playerName.Groups[2].Value);
			//string heroText = HttpRequest("http://localhost:4655/HoN/hero.html");
			Match killSet = Regex.Match(heroText, @"<div id=[""]kills_history[""][\s\S]*?</div>");
			string killText = killSet.Value;
			MatchCollection kills = Regex.Matches(killText, @"<li class=[""]cat[""][^>]+>([^<]+)(?:(?!\[)[^>]+>)+\[([^]]+)");
			foreach (Match kill in kills)
			{
				curGame.AddEvent("kill", kill.Groups[2].Value, kill.Groups[1].Value);
			}

			string heroName = Regex.Match(heroText, @"Hero Played[^>]+>[^>]+>[^>]+>([^<]+)").Groups[1].Value;

			curGame.AddPlayer(playerName.Groups[1].Value, heroName);
			break;
		}
    }

	string HttpRequest(string url)
	{
		return HttpRequest("GET", url, string.Empty);
	}

	string HttpRequest(string method, string url, string requestBody)
	{
		WebRequest request = WebRequest.Create(url);
		request.Method = method;
		if (method == "POST") request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

		if (!string.IsNullOrEmpty(requestBody))
		{
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] data = encoding.GetBytes(requestBody);

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(data, 0, data.Length);
			requestStream.Close();
		}

		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		Stream dataStream = response.GetResponseStream();
		StreamReader reader = new StreamReader(dataStream);
		string serverResponse = reader.ReadToEnd();
		reader.Close();
		dataStream.Close();
		response.Close();
		return serverResponse;
	}
}
