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

namespace sf.net.hsqlado
{
	public class HsqlParameterCollection : IDataParameterCollection
	{
		private ArrayList parameters = new ArrayList(255);

		private Hashtable indexHash = new Hashtable(new CaseInsensitiveHashCodeProvider(),
		                                            new CaseInsensitiveComparer());

		public bool Contains(string parameterName)
		{
			return indexHash.ContainsKey(parameterName);
		}

		public int IndexOf(string parameterName)
		{
			return parameters.IndexOf(parameterName);
		}

		public void Remove(object value)
		{
			HsqlParameter p = ValidateParameter(value);

			RemoveAt(InternalIndexOf(p.ParameterName));
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || (index > (parameters.Count - 1)))
			{
				throw new ArgumentOutOfRangeException();
			}

			HsqlParameter p = (HsqlParameter) parameters[index];

			indexHash.Remove(p.ParameterName);
			parameters.RemoveAt(index);

			//May not be a good idea for large collections
			for (int i = 0; i < parameters.Count; i++)
			{
				p = (HsqlParameter) parameters[i];
				indexHash[p.ParameterName] = i;
			}
		}

		public void RemoveAt(string parameterName)
		{
			RemoveAt(InternalIndexOf(parameterName));
		}

		public int Add(object value)
		{
			int idx;

			HsqlParameter p = ValidateParameter(value);

			if (indexHash.ContainsKey(p.ParameterName))
			{
				idx = Convert.ToInt16(indexHash[p.ParameterName]);
				parameters[idx] = value;
			}
			else
			{
				parameters.Add(value);
				indexHash.Add(p.ParameterName, parameters.Count - 1);

				idx = parameters.Count - 1;
			}

			return idx;
		}

		public bool Contains(object value)
		{
			if (value == null)
			{
				return false;
			}

			if (!(value is HsqlParameter))
			{
				return false;
			}

			HsqlParameter p = (HsqlParameter) value;

			return indexHash.ContainsKey(p.ParameterName);
		}

		public void Clear()
		{
			parameters.Clear();
			indexHash.Clear();
		}

		public int IndexOf(object value)
		{
			int idx = -1;

			HsqlParameter p = ValidateParameter(value);

			if (indexHash.ContainsKey(p.ParameterName))
			{
				idx = Convert.ToInt16(indexHash[p.ParameterName]);
			}

			return idx;
		}

		public void Insert(int index, object value)
		{
			HsqlParameter p = ValidateParameter(value);

			parameters.Insert(index, value);
			indexHash[p.ParameterName] = index;
		}

		object IDataParameterCollection.this[string name]
		{
			get { return this[name]; }
			set
			{
				HsqlParameter p = ValidateParameter(value);
				this[name] = p;
			}
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set
			{
				HsqlParameter p = ValidateParameter(value);
				this[index] = p;
			}
		}

		public HsqlParameter this[int index]
		{
			get { return (HsqlParameter) parameters[index]; }
			set
			{
				if (index < 0 || (index > (parameters.Count - 1)))
				{
					throw new ArgumentOutOfRangeException();
				}

				HsqlParameter p = ValidateParameter(value);

				parameters[index] = p;
			}
		}

		public HsqlParameter this[string parameterName]
		{
			get { return (HsqlParameter) parameters[InternalIndexOf(parameterName)]; }
			set
			{
				HsqlParameter p = ValidateParameter(value);

				parameters[InternalIndexOf(parameterName)] = p;
			}
		}

		bool IList.IsReadOnly
		{
			get { return parameters.IsReadOnly; }
		}

		bool IList.IsFixedSize
		{
			get { return parameters.IsFixedSize; }
		}

		public void CopyTo(Array array, int index)
		{
			parameters.CopyTo(array, index);
		}

		public int Count
		{
			get { return parameters.Count; }
		}

		object ICollection.SyncRoot
		{
			get { return parameters.SyncRoot; }
		}

		bool ICollection.IsSynchronized
		{
			get { return parameters.IsSynchronized; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return parameters.GetEnumerator();
		}

		private HsqlParameter ValidateParameter(object value)
		{
			if (value == null)
			{
				throw new ArgumentException("The HsqlParameterCollection only accepts non-null HsqlParameter type objects.", "value");
			}

			if (!(value is HsqlParameter))
			{
				throw new ArgumentException("The argument is not a HsqlParameter object.", "value");
			}

			HsqlParameter p = (HsqlParameter) value;

			if (p.ParameterName == null || p.ParameterName == String.Empty)
			{
				throw new ArgumentException("The HsqlParameter must have a name.");
			}

			return p;
		}

		private int InternalIndexOf(string parameterName)
		{
			int idx = IndexOf(parameterName);

			if (idx != -1)
			{
				return idx;
			}

			if (indexHash.ContainsKey(parameterName))
			{
				idx = Convert.ToInt16(indexHash[parameterName]);
				return idx;
			}

			throw new ArgumentOutOfRangeException("An HsqlParameter with ParameterName '" + parameterName +
			                                      "' is not contained by this HsqlParameterCollection.");
		}
	}
}