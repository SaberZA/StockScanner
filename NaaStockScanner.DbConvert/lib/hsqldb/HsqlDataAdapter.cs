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

using System.Data;
using System.Data.Common;

namespace sf.net.hsqlado
{
	[System.ComponentModel.DesignerCategory("Code")]
	public sealed class HsqlDataAdapter : DbDataAdapter, IDbDataAdapter
	{
		private IDbCommand deleteCommand;
		private IDbCommand updateCommand;
		private IDbCommand insertCommand;
		private IDbCommand selectCommand;

		private static readonly object EventRowUpdated = new object();
		private static readonly object EventRowUpdating = new object();

		public delegate void HsqlRowUpdatingEventHandler(object sender, HsqlRowUpdatingEventArgs e);

		public delegate void HsqlRowUpdatedEventHandler(object sender, HsqlRowUpdatedEventArgs e);

		public HsqlDataAdapter()
		{
		}

		public HsqlDataAdapter(HsqlCommand selectCommand) : base()
		{
			this.selectCommand = selectCommand;
		}

		public HsqlDataAdapter(string selectCommandText, HsqlConnection connection) : base()
		{
			selectCommand = new HsqlCommand(selectCommandText, connection);
		}

		public HsqlDataAdapter(string selectCommandText, string selectConnectionString) : base()
		{
			selectCommand = new HsqlCommand(selectCommandText,
			                                new HsqlConnection(selectConnectionString));
		}

		public IDbCommand SelectCommand
		{
			get { return selectCommand; }
			set { selectCommand = value; }
		}

		public IDbCommand InsertCommand
		{
			get { return insertCommand; }
			set { insertCommand = value; }
		}

		public IDbCommand UpdateCommand
		{
			get { return updateCommand; }
			set { updateCommand = value; }
		}

		public IDbCommand DeleteCommand
		{
			get { return deleteCommand; }
			set { deleteCommand = value; }
		}

		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command,
		                                                             StatementType statementType,
		                                                             DataTableMapping tableMapping)
		{
			return new HsqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command,
		                                                               StatementType statementType,
		                                                               DataTableMapping tableMapping)
		{
			return new HsqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			HsqlRowUpdatedEventArgs margs = (value as HsqlRowUpdatedEventArgs);

			HsqlRowUpdatedEventHandler handler = (HsqlRowUpdatedEventHandler) Events[EventRowUpdated];
			if ((null != handler) && (margs != null))
			{
				handler(this, margs);
			}
		}

		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			HsqlRowUpdatingEventArgs margs = (value as HsqlRowUpdatingEventArgs);

			HsqlRowUpdatingEventHandler handler = (HsqlRowUpdatingEventHandler) Events[EventRowUpdating];
			if ((null != handler) && (margs != null))
			{
				handler(this, margs);
			}
		}

		public event HsqlRowUpdatingEventHandler RowUpdating
		{
			add { Events.AddHandler(EventRowUpdating, value); }
			remove { Events.RemoveHandler(EventRowUpdating, value); }
		}

		public event HsqlRowUpdatedEventHandler RowUpdated
		{
			add { Events.AddHandler(EventRowUpdated, value); }
			remove { Events.RemoveHandler(EventRowUpdated, value); }
		}
	}
}