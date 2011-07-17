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
using System.Collections.Generic;

/// <summary>
/// Summary description for HoN
/// </summary>
namespace HoN
{
	public class Game
	{
		public string Id { set; get; }
		public List<Player> Players = new List<Player>();
		public List<TimeFrame> TimeLine = new List<TimeFrame>();
		public void AddPlayer(string playerName, string heroName)
		{
			Hero myHero = new Hero();
			myHero.Name = heroName;

			Player myPlayer = new Player();
			myPlayer.Name = playerName;
			myPlayer.Hero = myHero;

			Players.Add(myPlayer);
		}
		public void AddEvent(string type, string period, string info)
		{
			TimeFrame myTimeFrame = new TimeFrame();
			myTimeFrame.Type = type;
			myTimeFrame.Period = period;
			myTimeFrame.Info = info;
			TimeLine.Add(myTimeFrame);
		}
	}
	public class Hero
	{
		public string Name { set; get; }
	}
	public class Player
	{
		public string Name { set; get; }
		public Hero Hero { set; get; }
	}
	public class TimeFrame
	{
		public string Type { set; get; }
		public string Period { set; get; }
		public string Info { set; get; }
	}
}
