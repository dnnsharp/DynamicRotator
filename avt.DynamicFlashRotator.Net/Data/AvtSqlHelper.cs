
using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;


namespace avt.DynamicFlashRotator.Net.Data
{
    public class AvtSqlHelper_Table
    {
        string _ConnString;

        string _Table;
        string[] _AllColumns;
        string[] _Columns;
        string[] _IdColumns;
        bool _IsIdentity;

        public AvtSqlHelper_Table(string connString, string table, bool isIdentity, string[] idColumns, params string[] columns)
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
                sbSql.AppendFormat("INSERT INTO {0} ({1}) VALUES ", _Table, string.Join(",", _AllColumns));
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
                return Convert.ToInt32(ExecuteScalar(_ConnString, CommandType.Text, sql, null));
            } else {
                ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
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
            ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        public int Update(object[] pKeys, params object[] data)
        {
            string sql;
            if (_IsIdentity) {
                sql = ((int)pKeys[0]) > 0 ? SqlEdit(pKeys, data) : SqlAdd(pKeys, data);
            } else {
                sql = @"
                IF EXISTS (SELECT 1 FROM " + _Table + " Where " + JoinConditions(" AND ", _IdColumns, pKeys) + @")
                    " + SqlEdit(pKeys, data) + @"
                ELSE
                    " + SqlAdd(pKeys, data) + @"
                ";
            }
            if (_IsIdentity) {
                return Convert.ToInt32(ExecuteScalar(_ConnString, CommandType.Text, sql, null));
            } else {
                ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
                return -1;
            }
        }

        public void UpdateField(string fieldName, object data, string whereCond)
        {
            string sql = string.Format("UPDATE {0} SET {1}={2} {3}", _Table, fieldName, EncodeSql(data), string.IsNullOrEmpty(whereCond) ? "" : "WHERE " + whereCond);
            ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        public void Delete(object[] pKeys)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1}", _Table, JoinConditions(" AND ", _IdColumns, pKeys));
            ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        public void Delete(string whereCond)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1}", _Table, whereCond);
            ExecuteNonQuery(_ConnString, CommandType.Text, sql, null);
        }

        // TODO: enhance this with specifics (where, orders, etc)
        public IDataReader Get(string appendSql)
        {
            string sql = string.Format("SELECT {1} FROM {0} {2}", _Table, string.Join(",", _AllColumns), appendSql);
            return ExecuteReader(_ConnString, CommandType.Text, sql, null);
        }

        public object GetScalar(string column, string appendSql)
        {
            string sql = string.Format("SELECT {1} FROM {0} {2}", _Table, column, appendSql);
            return ExecuteScalar(_ConnString, CommandType.Text, sql, null);
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

        public static string EncodeSql(object data)
        {
            Type dataType;
            try {
                dataType = data.GetType();
            } catch {
                return "NULL";
            }

            if (dataType == typeof(string) || dataType == typeof(DateTime)) {
                return string.Format("'{0}'", data.ToString().Replace("'", "''"));
            } else if (dataType == typeof(int) || dataType == typeof(double)) {
                return data.ToString();
            } else if (dataType == typeof(bool)) {
                return (bool)data ? "1" : "0";
            }

            throw new Exception("Input data type not supported for update");
        }



        #region SQL Helpers

        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = null;
            SqlDataReader reader;
            if ((connectionString == null) || (connectionString.Length == 0)) {
                throw new ArgumentNullException("connectionString");
            }
            try {
                connection = new SqlConnection(connectionString);
                connection.Open();
                reader = ExecuteReader(connection, null, commandType, commandText, commandParameters);
            } catch (Exception) {
                if (connection != null) {
                    connection.Dispose();
                }
                throw;
            }
            return reader;
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            SqlDataReader reader;
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand command = new SqlCommand();
            try {
                SqlDataReader reader2;
                PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
                reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);

                bool flag2 = true;
                foreach (SqlParameter parameter in command.Parameters) {
                    if (parameter.Direction != ParameterDirection.Input) {
                        flag2 = false;
                    }
                }
                if (flag2) {
                    command.Parameters.Clear();
                }
                reader = reader2;
            } catch (Exception) {
                if (mustCloseConnection) {
                    connection.Close();
                }
                throw;
            }
            return reader;
        }


        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0)) {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters, ref mustCloseConnection);
            int num2 = command.ExecuteNonQuery();
            command.Parameters.Clear();
            if (mustCloseConnection) {
                connection.Close();
            }
            return num2;
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0)) {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters, ref mustCloseConnection);
            object objectValue = System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(command.ExecuteScalar());
            command.Parameters.Clear();
            if (mustCloseConnection) {
                connection.Close();
            }
            return objectValue;
        }

        static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, ref bool mustCloseConnection)
        {
            if (command == null) {
                throw new ArgumentNullException("command");
            }
            if ((commandText == null) || (commandText.Length == 0)) {
                throw new ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open) {
                connection.Open();
                mustCloseConnection = true;
            } else {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null) {
                if (transaction.Connection == null) {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null) {
                foreach (SqlParameter parameter in commandParameters) {
                    if (parameter != null) {
                        if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null)) {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }
        }

        #endregion

    }


}
