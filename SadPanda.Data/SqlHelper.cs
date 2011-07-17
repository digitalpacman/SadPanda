using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace SadPanda.Data.Sql
{
    public static class SqlHelper
	{
        public static MySqlDataReader ExecuteReader(string sql, params object[] replacementList)
		{
			sql = FillParams(sql, replacementList);

			MySqlConnection connection = new MySqlConnection(GetConnectionString());
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.CommandType = System.Data.CommandType.Text;
			MySqlDataReader dataReader = command.ExecuteReader();
			command.Dispose();
			connection.Dispose();
			return dataReader;
		}

		public static object MySqlExecuteScalar(string sql, params object[] replacementList)
		{
			sql = FillParams(sql, replacementList);

			MySqlConnection connection = new MySqlConnection(GetConnectionString());
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.CommandType = System.Data.CommandType.Text;
			object scalar = command.ExecuteScalar();
			command.Dispose();
			connection.Dispose();
			return scalar;
		}

		public static void ExecuteNonQuery(string sql, params object[] replacementList)
		{
			sql = FillParams(sql, replacementList);

			MySqlConnection connection = new MySqlConnection(GetConnectionString());
			connection.Open();
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.CommandType = System.Data.CommandType.Text;
			MySqlDataReader dataReader = command.ExecuteReader();
			command.Dispose();
			connection.Dispose();
		}

		private static string FillParams(string sql, params object[] list)
		{
			if (list.Length > 0)
			{
				for (int i = 0; i < list.Length; i++)
				{
					if (list[i].GetType().ToString() == "System.String")
					{
						list[i] = list[i].ToString().Replace("'", "''");
					}
				}
				sql = string.Format(sql, list);
			}
			return sql;
		}

		private static string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["SqlServices"].ConnectionString;
		}
    }
}
