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
using System.Data;
using sf.net.hsqlado.driver;
using sf.net.hsqlado.exceptions;

namespace sf.net.hsqlado
{
	public sealed class HsqlCommand : IDbCommand
	{
		private UpdateRowSource updatedRowSource;
		private HsqlParameterCollection parameters = new HsqlParameterCollection();
		private CommandType commandType = CommandType.Text;
		private int commandTimeout;
		private string commandText;
		private IDbTransaction transaction;
		private HsqlConnection connection;

		HsqlStatement preparedStatement;

		public HsqlCommand(HsqlConnection connection)
		{
			this.connection = connection;
		}

		public HsqlCommand(String commandText, HsqlConnection connection)
		{
			this.commandText = commandText;
			this.connection = connection;
		}

		public HsqlCommand(string commandText)
		{
			this.commandText = commandText;
		}

		public void Prepare()
		{
			if (commandText == null || "".Equals(commandText.Trim()))
			{
				throw new HsqlException("Command text is not initialized.");
			}

			preparedStatement = new HsqlStatement(commandText, parameters);
			PrepareStatement(preparedStatement);
		}

		public void Cancel()
		{
			throw new NotSupportedException("This method is not supported by the HSQL Database engine.");
		}

		public IDbDataParameter CreateParameter()
		{
			return new HsqlParameter();
		}

		public int ExecuteNonQuery()
		{
			HsqlStatement statement;

			if (preparedStatement != null)
			{
				statement = preparedStatement;
			}
			else
			{
				statement = new HsqlStatement(commandText);
			}

			statement.MaxRows = 1;

			HsqlResult result = Execute(statement);

			return result.UpdateCount;
		}

		public IDataReader ExecuteReader()
		{
			return ExecuteReader(CommandBehavior.Default);
		}

		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			HsqlStatement statement;
			HsqlDataReader reader;
			HsqlResult result;

			bool isSchemaOnly = false;
			bool isSingleResult = false;
			int maxRows = HsqlResultConstants.MAX_ROWS_NO_LIMIT;

			if (0 != (behavior & CommandBehavior.SchemaOnly))
			{
				isSchemaOnly = true;
			}

			if (0 != (behavior & CommandBehavior.SingleResult))
			{
				isSingleResult = true;
			}

			if (0 != (behavior & CommandBehavior.SingleRow))
			{
				maxRows = 1;
			}

			if (preparedStatement != null)
			{
				statement = preparedStatement;
			}
			else
			{
				statement = new HsqlStatement(commandText);
			}

			statement.IsSingleResult = isSingleResult;
			statement.IsSchemaOnly = isSchemaOnly;
			statement.MaxRows = maxRows;

			result = Execute(statement);
			reader = new HsqlDataReader(result, connection, behavior);

			return reader;
		}

		public object ExecuteScalar()
		{
			//TODO: HSQL DB support single column results, 
			//		need to find out how to create a request packet for this
			
			HsqlStatement statement;

			if (preparedStatement != null)
			{
				statement = preparedStatement;
			}
			else
			{
				statement = new HsqlStatement(commandText);
			}

			statement.MaxRows = 1;
			HsqlResult result = Execute(statement);

			return result.Root.Data[0];
		}

		public IDbConnection Connection
		{
			get { return connection; }
			set { connection = (HsqlConnection) value; }
		}

		public IDbTransaction Transaction
		{
			get { return transaction; }
			set { transaction = value; }
		}

		public string CommandText
		{
			get { return commandText; }
			set { commandText = value; }
		}

		public int CommandTimeout
		{
			get { return commandTimeout; }
			set { commandTimeout = value; }
		}

		public CommandType CommandType
		{
			get { return commandType; }
			set
			{
				//TODO: research if TableDirect type can be implemented
			}
		}

		public HsqlParameterCollection Parameters
		{
			get { return parameters; }
		}

		IDataParameterCollection IDbCommand.Parameters
		{
			get { return parameters; }
		}

		public UpdateRowSource UpdatedRowSource
		{
			get { return updatedRowSource; }
			set { updatedRowSource = value; }
		}

		public void Dispose()
		{
		}

		private HsqlResult Execute(HsqlStatement statement)
		{
			HsqlResult result;

			if (commandText == null || "".Equals(commandText.Trim()))
			{
				throw new HsqlException("Command text is not initialized.");
			}

			if (connection != null && connection.State == ConnectionState.Closed)
			{
				throw new HsqlException("Connection is closed.");
			}

			connection.SetConnectionState(ConnectionState.Fetching);

			result = connection.HsqlClient.ExecuteStatement(statement);

			connection.SetConnectionState(ConnectionState.Open);

			return result;
		}

		private void PrepareStatement(HsqlStatement statement)
		{
			if (connection != null && connection.State == ConnectionState.Closed)
			{
				throw new HsqlException("Connection is closed.");
			}

			connection.HsqlClient.PrepareStatement(statement);
		}
	}
}