using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using avt.DynamicFlashRotator.Net.RegCore;

namespace avt.DynamicFlashRotator.Net.RegCore.Storage
{
    internal class DsDatabase : IActivationDataStore
    {
        string _conStr;
        string _dbo;
        string _qualifier;
        string _table;

        public DsDatabase(string conStr, string dbo, string qualifier, string table)
        {
            _conStr = conStr;
            _dbo = dbo;
            _qualifier = qualifier;
            _table = table;
        }

        public Dictionary<string, LicenseActivation> GetActivations()
        {
            SqlConnection conn = new SqlConnection(_conStr);
            SqlDataAdapter a = new SqlDataAdapter("select * from " + _dbo + _qualifier + _table, conn);
            DataSet s = new DataSet(); a.Fill(s);

            Dictionary<string, LicenseActivation> activations = new Dictionary<string, LicenseActivation>();
            foreach (DataRow dr in s.Tables[0].Rows) {
                LicenseActivation act = new LicenseActivation();
                act.Host = dr["Host"].ToString();
                act.RegistrationCode = dr["RegistrationCode"].ToString();
                act.ActivationCode = dr["ActivationCode"].ToString();
                act.ProductKey = dr["ProductKey"].ToString();
                act.BaseProductVersion = dr["BaseProductVersion"].ToString();
                act.BaseProductCode = dr["BaseProductCode"].ToString();
                //Console.WriteLine(dr[0].ToString());
                activations[act.Host] = act;
            }

            return activations;
        }

        public void AddActivation(string regCode, string host, string actCode, string productKey, string baseProductCode, string baseVersionCode)
        {
            string sqlF = "DELETE FROM {0} WHERE RegistrationCode = '{1}' AND Host = '{2}'; INSERT INTO {0} VALUES('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table, regCode, host.Replace("'", "''"), actCode.Replace("'", "''"), productKey.Replace("'", "''"), baseProductCode, baseVersionCode);
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void AddActivation(LicenseActivation act)
        {
            AddActivation(act.RegistrationCode, act.Host, act.ActivationCode, act.ProductKey, act.BaseProductCode, act.BaseProductVersion);
        }


        public void Remove(string regCode, string host)
        {
            string sqlF = "DELETE FROM {0} WHERE RegistrationCode = '{1}' AND Host = '{2}'; ";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table, regCode, host.Replace("'", "''"));
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void RemoveTrial()
        {
            string sqlF = "DELETE FROM {0} WHERE RegistrationCode LIKE 'DAY-'";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table);
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Remove(LicenseActivation act)
        {
            Remove(act.RegistrationCode, act.Host);
        }

        public void RemoveAll()
        {
            string sqlF = "DELETE FROM {0}";
            string sql = string.Format(sqlF, _dbo + _qualifier + _table);
            SqlConnection conn = new SqlConnection(_conStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}