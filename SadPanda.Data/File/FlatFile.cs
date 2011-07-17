using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SqlHelper.File
{
	public static class FlatFile
	{
		private static string rowSeperator = Environment.NewLine;
		private static string columnSeperator = "\t|\t";

		public static DataTable GetTable(string path)
		{
			return GetTable(path, false);
		}

		public static void CreateTable(string path, params string[] columns)
		{
			if (System.IO.File.Exists(path)) return;
			System.IO.File.Create(path).Close();
			System.IO.File.WriteAllText(path, string.Join(columnSeperator, columns));
		}

		public static DataTable GetTable(string path, bool create)
		{
			DataTable data = new DataTable();

			if (!System.IO.File.Exists(path))
				if (create)
					System.IO.File.Create(path).Close();
				else
					return data;
			
			string[] rowDelimiter = new string[1];
			string[] columnDelimiter = new string[1];
			rowDelimiter[0] = rowSeperator;
			columnDelimiter[0] = columnSeperator;

			string contents = System.IO.File.ReadAllText(path);
			string[] rows = contents.Split(rowDelimiter, StringSplitOptions.None);

			// header
			string[] columns = rows[0].Split(columnDelimiter, StringSplitOptions.None);
			columns.All(h => { data.Columns.Add(h); return true; });

			for (int i = 1; i < rows.Length; i++)
				data.Rows.Add(rows[i].Split(columnDelimiter, StringSplitOptions.None));

			return data;
		}

		public static void SaveTable(string path, DataTable data)
		{
			StringBuilder contents = new StringBuilder();

			foreach (DataColumn column in data.Columns)
			{
				contents.Append(column.ColumnName);
			}
			for (int i = 0; i < data.Rows.Count; i++)
			{
				DataRow row = data.Rows[i];
				contents.Append(rowSeperator);
				for (int x = 0; x < data.Columns.Count; x++)
				{
					if (x > 0) contents.Append(columnSeperator + row[x].ToString());
					else contents.Append(row[x].ToString());
				}

			}
			System.IO.File.WriteAllText(path, contents.ToString());
		}
	}
}
