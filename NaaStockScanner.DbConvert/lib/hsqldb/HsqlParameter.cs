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

namespace sf.net.hsqlado
{
	public class HsqlParameter : IDataParameter, IDbDataParameter
	{
		private int size;
		private byte scale;
		private byte precision;
		private object value;
		private DataRowVersion sourceVersion = DataRowVersion.Current;
		private string sourceColumn;
		private string parameterName;
		private bool isNullable = false;
		private ParameterDirection direction = ParameterDirection.Input;
		private DbType dbType;

		public HsqlParameter()
		{
		}

		public HsqlParameter(string parameterName, object value)
		{
			this.value = value;
			this.parameterName = parameterName;
		}

		public DbType DbType
		{
			get { return dbType; }
			set { dbType = value; }
		}

		public ParameterDirection Direction
		{
			get { return direction; }
			set
			{
				if (value != ParameterDirection.Input)
					throw new ArgumentException("Only input parameters are supported by the HSQL database engine");

				direction = value;
			}
		}

		public bool IsNullable
		{
			get { return isNullable; }
		}

		public string ParameterName
		{
			get { return parameterName; }
			set { parameterName = value; }
		}

		public string SourceColumn
		{
			get { return sourceColumn; }
			set { sourceColumn = value; }
		}

		public DataRowVersion SourceVersion
		{
			get { return sourceVersion; }
			set
			{
				if (value != DataRowVersion.Current)
					throw new ArgumentException("Only current data row version is supported by the HSQL database engine");

				sourceVersion = value;
			}
		}

		public object Value
		{
			get { return value; }
			set { this.value = value; }
		}

		public byte Precision
		{
			get { return precision; }
			set { precision = value; }
		}

		public byte Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		public int Size
		{
			get { return size; }
			set { size = value; }
		}

		public override string ToString()
		{
			return ParameterName;
		}
	}
}