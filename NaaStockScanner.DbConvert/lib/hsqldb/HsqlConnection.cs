// HSQL ADO.Net Data Provider
// Copyright (c) 2007 Jesse Martinez
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections;
using System.Data;
using sf.net.hsqlado.driver;
using sf.net.hsqlado.exceptions;
using sf.net.hsqlado.utils;

namespace sf.net.hsqlado
{
	public sealed class HsqlConnection : IDbConnection
	{
		private HsqlClient hsqlClient = new HsqlClient();

		private const int DEFAULT_PORT = 9001;
		private const string DEFAULT_SERVER = "127.0.0.1";

		private ConnectionState state;
		private string userName = "";
		private string userPassword = "";
		private int port = DEFAULT_PORT;
		private string server = DEFAULT_SERVER;
		private string database = "";
		private int connectionTimeout = 0;
		private string connectionString = "";

		public HsqlConnection()
		{
		}

		public HsqlConnection(string connectionString)
		{
			this.connectionString = connectionString;
		}

		internal HsqlClient HsqlClient
		{
			get { return hsqlClient; }
		}

		public IDbTransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			if (state != ConnectionState.Closed)
			{
				hsqlClient.Close();

				state = ConnectionState.Closed;
			}
		}

		public void ChangeDatabase(string databaseName)
		{
			throw new NotSupportedException(
				"Changing database is not supported by the HSQL Database engine. Try opening the Connection with a new connection string.");
		}

		public IDbCommand CreateCommand()
		{
			return new HsqlCommand(this);
		}

		public void Open()
		{
			if (state == ConnectionState.Open)
			{
				throw new Exception("Connection already open.");
			}

			if (connectionString == null || "".Equals(connectionString.Trim()))
			{
				throw new HsqlException("Connection string is empty");
			}

			if (!ParseConnectionString())
			{
				throw new HsqlException("Connection string is not well formed.");
			}

			state = ConnectionState.Connecting;

			try
			{
				hsqlClient.Connect(
					server, port, userName, userPassword, database);
			}
			catch (Exception e)
			{
				state = ConnectionState.Closed;
				throw e;
			}

			state = ConnectionState.Open;
		}

		public string ConnectionString
		{
			get { return connectionString; }
			set
			{
				if (state == ConnectionState.Closed)
				{
					connectionString = value;
				}
			}
		}

		public int ConnectionTimeout
		{
			get { return connectionTimeout; }
		}

		public string Database
		{
			get { return database; }
		}

		public ConnectionState State
		{
			get { return state; }
		}

		internal void SetConnectionState(ConnectionState state)
		{
			this.state = state;
		}

		public void Dispose()
		{
			if (State == ConnectionState.Open)
			{
				Close();
			}
		}

		private bool ParseConnectionString()
		{
			bool isConnectionStringWellFormed = true;
			IDictionary connectionParams;

			connectionParams = StringUtil.ParseKeyValuePairs(connectionString);

			foreach (DictionaryEntry param in connectionParams)
			{
				string key = (string) param.Key;

				switch (key)
				{
					case "user":
					case "user id":
						userName = param.Value.ToString().Trim();
						break;
					case "password":
					case "pwd":
						userPassword = param.Value.ToString().Trim();
						break;
					case "server":
					case "data source":
						server = param.Value.ToString().Trim();
						break;
					case "port":
						port = Convert.ToInt32(param.Value);
						break;
					case "database":
					case "initial catalog":
						database = param.Value.ToString().Trim();
						break;
				}
			}

			if ("".Equals(userName) ||
			    "".Equals(database))
			{
				isConnectionStringWellFormed = false;
			}

			return isConnectionStringWellFormed;
		}
	}
}