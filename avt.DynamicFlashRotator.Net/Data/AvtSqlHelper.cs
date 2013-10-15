using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace avt.DynamicFlashRotator.Net.Data
{
    public class SqlTable
    {
        string _ConnString;

        string _Table;
        string[] _AllColumns;
        string[] _Columns;
        string[] _IdColumns;
        bool _IsIdentity;

        public SqlTable(string connString, string table, bool isIdentity, string[] idColumns, params string[] columns)
        {
            _ConnString = connString;

            _Table = table;
            _IsIdentity = isIdentity;
            _Columns = columns;
            _IdColumns = idColumns;

            List<string> allCols = new List<string>();
            allCols.AddRange(_IdColumns);
            allCols.AddRange(_Columns);
            _AllColumns = allCols.ToArray();
        }

        string SqlAdd(object[] pKeys, params object[] data)
        {
            // assert
            if (data.Length != _Columns.Length)
                throw new Exception("Number of input parameters does not match number of columns for add");

            StringBuilder sbSql = new StringBuilder();
            if (_IsIdentity) {
                sbSql.AppendFormat("INSERT INTO {0} ({1}) VALUES ", _Table, string.Join(",", _Columns));
            } else {
                // append insert check to avoid duplicates
                sbSql.AppendFormat("IF NOT EXISTS(Select 1 FROM {0} WHERE ", _Table);
                for (int i = 0; i < _IdColumns.Length; i++) {
                    sbSql.AppendFormat("{0}={1}", _IdColumns[i], EncodeSql(pKeys[i]));
                    if (i < _IdColumns.Length - 1)
                        sbSql.Append(" AND ");
                }

                sbSql.AppendFormat(") INSERT INTO {0} ({1}) VALUES ", _Table, string.Join(",", _AllColumns));
            }

            // append input
            sbSql.Append('(');
            if (!_IsIdentity) {
                foreach (object pKey in pKeys) {
                    sbSql.AppendFormat("{0},", EncodeSql(pKey));
                }
            }
            for (int i = 0; i < data.Length; i++) {
                sbSql.AppendFormat("{0},", EncodeSql(data[i]));
            }

            // remove last coma
            if (sbSql[sbSql.Length - 1] == ',')
                sbSql = sbSql.Remove(sbSql.Length - 1, 1);

            sbSql.Append(')');
            sbSql.Append("\n Select SCOPE_IDENTITY()");

            return sbSql.ToString();
        }

        public int Add(object[] pKeys, params object[] data)
        {
            string sql = SqlAdd(pKeys, data);
            if (_IsIdentity) {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(_ConnString, CommandType.Text, sql, null));
            } else {
                SqlHelper.ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
            }
            return -1;
        }

        string SqlEdit(object[] pKeys, params object[] data)
        {
            // assert
            if (data.Length != _Columns.Length || pKeys.Length != _IdColumns.Length)
                throw new Exception("Number of input parameters does not match number of columns for update");

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("UPDATE {0} SET ", _Table);

            // append input
            for (int i = 0; i < data.Length; i++) {
                sbSql.AppendFormat("{0}={1},", _Columns[i], EncodeSql(data[i]));
            }

            // remove last coma
            if (sbSql[sbSql.Length - 1] == ',')
                sbSql = sbSql.Remove(sbSql.Length - 1, 1);

            // append wheere
            sbSql.Append(" WHERE ");
            sbSql.Append(JoinConditions(" AND ", _IdColumns, pKeys));

            if (_IsIdentity)
                sbSql.AppendFormat("\n \n SELECT {0}", pKeys[0]);

            return sbSql.ToString();
        }

        public void Edit(object[] pKeys, params object[] data)
        {
            string sql = SqlEdit(pKeys, data);
            SqlHelper.ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        public int Update(object[] pKeys, params object[] data)
        {
            string sql = SqlUpdate(pKeys, data);

            if (_IsIdentity) {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(_ConnString, CommandType.Text, sql, null));
            } else {
                SqlHelper.ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
                return -1;
            }
        }

        public string SqlUpdate(object[] pKeys, params object[] data)
        {
            if (_IsIdentity)
                return ((int)pKeys[0]) > 0 ? SqlEdit(pKeys, data) : SqlAdd(pKeys, data);

            return @"
                IF EXISTS (SELECT 1 FROM " + _Table + " Where " + JoinConditions(" AND ", _IdColumns, pKeys) + @")
                    " + SqlEdit(pKeys, data) + @"
                ELSE
                    " + SqlAdd(pKeys, data) + @"
                ";
        }

        public void UpdateField(string fieldName, object data, string whereCond)
        {
            string sql = string.Format("UPDATE {0} SET {1}={2} {3}", _Table, fieldName, EncodeSql(data), string.IsNullOrEmpty(whereCond) ? "" : "WHERE " + whereCond);
            SqlHelper.ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        public void Delete(params object[] pKeys)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1}", _Table, JoinConditions(" AND ", _IdColumns, pKeys));
            SqlHelper.ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        public void Delete(string whereCond)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1}", _Table, whereCond);
            SqlHelper.ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        // TODO: enhance this with specifics (where, orders, etc)
        public IDataReader Get(string appendSql)
        {
            string sql = string.Format("SELECT {1} FROM {0} {2}", _Table, string.Join(",", _AllColumns), appendSql);
            return SqlHelper.ExecuteReader(_ConnString, CommandType.Text, sql, null);
        }

        public IDataReader Get(int top, string appendSql)
        {
            string sql = string.Format("SELECT TOP {3} {1} FROM {0} {2}", _Table, string.Join(",", _AllColumns), appendSql, top);
            return SqlHelper.ExecuteReader(_ConnString, CommandType.Text, sql, null);
        }

        public IDataReader GetDistinct(string[] columns, string appendSql)
        {
            string sql = string.Format("SELECT DISTINCT {1} FROM {0} {2}", _Table, string.Join(",", columns), appendSql);
            return SqlHelper.ExecuteReader(_ConnString, CommandType.Text, sql, null);
        }

        public object GetScalar(string column, string appendSql)
        {
            string sql = string.Format("SELECT {1} FROM {0} {2}", _Table, column, appendSql);
            return SqlHelper.ExecuteScalar(_ConnString, CommandType.Text, sql, null);
        }


        // UTILs

        string JoinConditions(string sep, string[] columns, object[] values)
        {
            if (columns.Length == 0 || columns.Length != values.Length) {
                throw new Exception("Invalid number of columns and values!");
            }

            StringBuilder sbCond = new StringBuilder();
            for (int i = 0; i < columns.Length; i++) {
                sbCond.AppendFormat("{0}={1}{2}", columns[i], EncodeSql(values[i]), sep);
            }

            sbCond = sbCond.Remove(sbCond.Length - sep.Length, sep.Length);
            return sbCond.ToString();
        }

        public static string EncodeSql(object data, bool appendQuotes = true)
        {
            Type dataType;
            try {
                dataType = data.GetType();
            } catch {
                return "NULL";
            }

            if (dataType == typeof(string)) {
                return string.Format(appendQuotes ? "N'{0}'" : "{0}", data.ToString().Replace("'", "''"));
            } else if (dataType == typeof(Guid)) {
                return string.Format(appendQuotes ? "N'{0}'" : "{0}", data.ToString());
            } else if (dataType == typeof(DateTime)) {
                return string.Format(appendQuotes ? "N'{0}'" : "{0}", ((DateTime)data).ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture).Replace("'", "''"));
            } else if (dataType == typeof(SqlDateTime)) {
                return string.Format(appendQuotes ? "N'{0}'" : "{0}", ((SqlDateTime)data).Value.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture).Replace("'", "''"));
            } else if (dataType == typeof(int)) {
                if ((int)data == int.MinValue)
                    return "NULL";
                return data.ToString();
            } else if (dataType == typeof(int?)) {
                if (((int?)data).HasValue)
                    return ((int?)data).Value.ToString();
                return "NULL";
            } else if (dataType == typeof(double)) {
                if ((double)data == double.MinValue)
                    return "NULL";
                return data.ToString();
            } else if (dataType == typeof(double?)) {
                if (((double?)data).HasValue)
                    return ((double?)data).Value.ToString();
                return "NULL";
            } else if (dataType == typeof(bool)) {
                return (bool)data ? "1" : "0";
            } else if (dataType == typeof(bool?)) {
                if (((bool?)data).HasValue)
                    return ((bool?)data).Value ? "1" : "0";
                return "NULL";
            }

            throw new Exception("Input data type not supported for update");
        }

    }
}
