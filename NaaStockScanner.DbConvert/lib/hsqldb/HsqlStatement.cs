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

using System.Collections;
using sf.net.hsqlado.driver;
using sf.net.hsqlado.exceptions;
using sf.net.hsqlado.utils;

namespace sf.net.hsqlado
{
	class HsqlStatement
	{
		private int statementId;
		private string commandText;
		private bool isPrepared;
		private bool isSchemaOnly = false;
		private bool isSingleResult = true;
		private HsqlMetadata parametersMetadata;
		private HsqlParameterCollection parameters;
		private IList parsedParameters;
		private int maxRows = HsqlResultConstants.MAX_ROWS_NO_LIMIT;

		public HsqlStatement(string commandText)
		{
			this.commandText = commandText;
			isPrepared = false;
		}

		public HsqlStatement(string commandText, HsqlParameterCollection parameters)
		{
			this.commandText = commandText;
			this.parameters = parameters;
			isPrepared = true;

			parsedParameters = StringUtil.ParseNamedParameters(commandText, '@');

			if (parsedParameters.Count < parameters.Count)
			{
				throw new HsqlException("Missing command parameters.");
			}
		}

		public int StatementId
		{
			set { statementId = value; }
			get { return statementId; }
		}

		public bool IsPrepared
		{
			get { return isPrepared; }
		}

		public bool IsSchemaOnly
		{
			get { return isSchemaOnly; }
			set { isSchemaOnly = value; }
		}

		public bool IsSingleResult
		{
			get { return isSingleResult; }
			set { isSingleResult = value; }
		}

		public int MaxRows
		{
			get { return maxRows; }
			set { maxRows = value; }
		}

		public HsqlMetadata ParametersMetadata
		{
			get { return parametersMetadata; }
			set { parametersMetadata = value; }
		}

		public override string ToString()
		{
			string tmp = commandText;

			if (isPrepared)
			{
				for (int i = 0; i < parsedParameters.Count; i++)
				{
					tmp = tmp.Replace(parsedParameters[i].ToString(), "?");
				}
			}

			return tmp;
		}

		public HsqlParameterCollection Parameters
		{
			get { return parameters; }
		}
	}
}