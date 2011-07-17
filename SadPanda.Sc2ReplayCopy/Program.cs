using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IWshRuntimeLibrary;

namespace Sc2ReplayCopy
{
	class Program
	{
		static string GetShortcutTargetPath(string shortcut)
		{
			if (System.IO.File.Exists(shortcut))
			{
				WshShell shell = new WshShell();
				IWshShortcut link = (IWshShortcut)shell.CreateShortcut(shortcut);
				return link.TargetPath;
			}
			return string.Empty;
		}

		static void Main(string[] args)
		{
			string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string starcraftIIDirectory = myDocumentsFolder + @"\StarCraft II";
			string destinationFolder = string.Empty;

			string userName;
			if (args.Length >= 1)
				userName = args[0].ToString();
			else
				userName = string.Empty;

			string[] starcraftIIUserShortcuts = Directory.GetFiles(starcraftIIDirectory, "*.lnk");
			starcraftIIUserShortcuts.All(p =>
			{
				string shortcutName = Path.GetFileNameWithoutExtension(p);
				if (string.IsNullOrEmpty(userName) || shortcutName.StartsWith(userName))
				{
					destinationFolder = string.Format("{0}\\Replays\\Multiplayer", GetShortcutTargetPath(p));
					return false;
				}

				return true;
			});

			if (string.IsNullOrEmpty(destinationFolder)) return;

			string[] sourceDirectories = {
				 string.Format(@"{0}\My Downloads", myDocumentsFolder),
				 string.Format(@"{0}\Downloads", myDocumentsFolder),
				 string.Format(@"{0}\Downloads", myDocumentsFolder.Substring(0, myDocumentsFolder.LastIndexOf("\\"))),
				 desktopFolder
			 };
			
			string[] replays = {};
			sourceDirectories.All(p =>
			{
				Console.Write(string.Format("Searching Directory: {0}{1}", p, Environment.NewLine));
				if (Directory.Exists(p))
				{
					Console.Write(string.Format("Adding Directory: {0}{1}", p, Environment.NewLine));
					replays = replays.Union(Directory.GetFiles(p, "*.SC2Replay")).ToArray();
				}
				return true;
			});

			if (args.Length >= 2)
			{
				args.All(p =>
				{
					Console.Write(string.Format("Searching Directory: {0}{1}", p, Environment.NewLine));
					if (Directory.Exists(p))
					{
						Console.Write(string.Format("Adding Directory: {0}{1}", p, Environment.NewLine));
						replays = replays.Union(Directory.GetFiles(p, "*.SC2Replay")).ToArray();
					}
					return true;
				});
			}

			replays.All(s =>
			{
				string fileName = System.IO.File.GetCreationTime(s).ToShortDateString().Replace('/', '-') + "-" + Path.GetFileName(s);
				string replayFile = destinationFolder + "\\" + fileName;

				if (System.IO.File.Exists(replayFile))
					System.IO.File.Delete(replayFile);
				System.IO.File.Move(s, destinationFolder + "\\" + fileName);
				Console.Write(string.Format("Moved Replay: {0} to {1}{2}", s, destinationFolder + "\\" + fileName, Environment.NewLine));
				return true;
			});
		}
	}
}
