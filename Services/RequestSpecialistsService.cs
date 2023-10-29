using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
	internal class RequestSpecialistsService
	{
		private readonly SqliteConnection _connection;

		/// <summary>
		/// Creates a new database operation instance, initializing the connection to the SQLite database.
		/// </summary>
		/// <param name="connectionString">The SQLite database connection string.</param>
		public RequestSpecialistsService(string connectionString)
		{
			_connection = new SqliteConnection(connectionString);

			try
			{
				_connection.Open();
			}
			catch (Exception ex)
			{
				throw new Exception("Error establishing database connection.", ex);
			}
		}
	}
}
