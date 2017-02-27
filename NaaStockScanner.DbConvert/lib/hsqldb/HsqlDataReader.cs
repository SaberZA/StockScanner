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

namespace sf.net.hsqlado
{
	public sealed class HsqlDataReader : IDataReader
	{
		private int fieldCount;
		private int recordsAffected;
		private bool isClosed;
		private int depth;
		private DataTable schemaTable;

		private CommandBehavior commandBehavior;
		private bool isFirstScan = true;

		private HsqlConnection connection;
		private HsqlResult currentResult;
		private HsqlRecord currentRecord;

		internal HsqlDataReader(HsqlResult result, HsqlConnection connection, CommandBehavior commandBehavior)
		{
			currentResult = result;
			currentRecord = currentResult.Root;
			this.connection = connection;
			this.commandBehavior = commandBehavior;

			recordsAffected = currentResult.UpdateCount;
			fieldCount = currentResult.Metadata.ColumnNames.Length;
			depth = currentResult.Size;
		}

		public void Close()
		{
			currentResult = null;
			currentRecord = null;

			if (0 != (commandBehavior & CommandBehavior.CloseConnection))
			{
				connection.Close();
			}

			isClosed = true;
		}

		public bool NextResult()
		{
			return false;//TODO: implement this method correctly
		}

		public bool Read()
		{
			if (currentResult.Root == null)
			{
				return false;
			}

			if (isFirstScan)
			{
				currentRecord = currentResult.Root;
				isFirstScan = false;

				return true;
			}

			if (currentRecord.Next == null)
			{
				return false;
			}
			else
			{
				currentRecord = currentRecord.Next;

				return true;
			}
		}

		public DataTable GetSchemaTable()
		{
			HsqlMetadata metadata = currentResult.Metadata;

			if (schemaTable != null)
			{
				return schemaTable;
			}

			DataTable table = new DataTable("SchemaTable");
			table.Columns.Add("columnName", typeof (string));
			table.Columns.Add("columnOrdinal", typeof (int));
			table.Columns.Add("columnType", typeof (string));
			table.Columns.Add("columnSize", typeof (int));
			table.Columns.Add("isNullable", typeof (bool));
			table.Columns.Add("isIdentity", typeof (bool));
			table.Columns.Add("isWritable", typeof (bool));

			for (int i = 0; i < fieldCount; i++)
			{
				DataRow row = table.NewRow();
				row["columnName"] = metadata.ColumnNames[i];
				row["columnOrdinal"] = i;
				row["columnType"] = HsqlTypes.GetDataTypeName(metadata.ColumnTypes[i]);
				row["columnSize"] = metadata.ColumnSizes[i];
				row["isNullable"] = metadata.ColumnNullable[i];
				row["isIdentity"] = metadata.IsIdentity[i];
				row["isWritable"] = metadata.IsWritable[i];

				table.Rows.Add(row);
			}

			schemaTable = table;

			return table;
		}

		public int Depth
		{
			get { return depth; }
		}

		public bool IsClosed
		{
			get { return isClosed; }
		}

		public int RecordsAffected
		{
			get { return recordsAffected; }
		}

		public void Dispose()
		{
			if (!isClosed)
			{
				Close();
			}

			if (schemaTable != null)
			{
				schemaTable.Dispose();
			}
		}

		public string GetName(int i)
		{
			if (currentResult.Metadata.ColumnNames.Length >= i)
			{
				return currentResult.Metadata.ColumnNames[i];
			}

			throw new IndexOutOfRangeException();
		}

		public string GetDataTypeName(int i)
		{
			if (currentResult.Metadata.ColumnTypes.Length >= i)
			{
				return HsqlTypes.GetDataTypeName(currentResult.Metadata.ColumnTypes[i]);
			}

			throw new IndexOutOfRangeException();
		}

		public Type GetFieldType(int i)
		{
			if (currentResult.Metadata.ColumnTypes.Length >= i)
			{
				return HsqlTypes.GetDataTypeType(currentResult.Metadata.ColumnTypes[i]);
			}

			throw new IndexOutOfRangeException();
		}

		public object GetValue(int i)
		{
			if (currentRecord.Data.Length >= i)
			{
				return currentRecord.Data[i];
			}

			throw new IndexOutOfRangeException();
		}

		public int GetValues(object[] values)
		{
			Array.Copy(currentRecord.Data, 0, values, 0, currentRecord.Data.Length);
			return currentRecord.Data.Length;
		}

		public int GetOrdinal(string name)
		{
			string[] colNames = currentResult.Metadata.ColumnNames;
			name = name.ToLower();

			for (int i = 0; i < colNames.Length; i++)
			{
				if (colNames[i].ToLower().Equals(name))
				{
					return i;
				}
			}

			throw new ArgumentOutOfRangeException();
		}

		public bool GetBoolean(int i)
		{
			object o = GetValue(i);

			if (o is bool)
			{
				return (bool) o;
			}

			throw new InvalidCastException();
		}

		public byte GetByte(int i)
		{
			object o = GetValue(i);

			if (o is byte)
			{
				return (byte) o;
			}

			throw new InvalidCastException();
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public char GetChar(int i)
		{
			object o = GetValue(i);

			if (o is char)
			{
				return (char) o;
			}

			throw new InvalidCastException();
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public Guid GetGuid(int i)
		{
			throw new NotSupportedException("Guids are not supported by the HSQL Database engine.");
		}

		public short GetInt16(int i)
		{
			object o = GetValue(i);

			if (o is Int16)
			{
				return (Int16) o;
			}

			throw new InvalidCastException();
		}

		public int GetInt32(int i)
		{
			object o = GetValue(i);

			if (o is Int32)
			{
				return (Int32) o;
			}

			throw new InvalidCastException();
		}

		public long GetInt64(int i)
		{
			object o = GetValue(i);

			if (o is Int64)
			{
				return (Int64) o;
			}

			throw new InvalidCastException();
		}

		public float GetFloat(int i)
		{
			object o = GetValue(i);

			if (o is float)
			{
				return (float) o;
			}

			throw new InvalidCastException();
		}

		public double GetDouble(int i)
		{
			object o = GetValue(i);

			if (o is double)
			{
				return (double) o;
			}

			throw new InvalidCastException();
		}

		public string GetString(int i)
		{
			object o = GetValue(i);

			if (o is string)
			{
				return (string) o;
			}

			throw new InvalidCastException();
		}

		public decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		public DateTime GetDateTime(int i)
		{
			object o = GetValue(i);

			if (o is DateTime)
			{
				return (DateTime) o;
			}

			throw new InvalidCastException();
		}

		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		public bool IsDBNull(int i)
		{
			object o = GetValue(i);

			return (o == null) ? true : false;
		}

		public int FieldCount
		{
			get { return fieldCount; }
		}

		public object this[int i]
		{
			get { return GetValue(i); }
		}

		public object this[string name]
		{
			get { return GetValue(GetOrdinal(name)); }
		}
	}
}