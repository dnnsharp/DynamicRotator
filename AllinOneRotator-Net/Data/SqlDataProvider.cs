using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace avt.AllinOneRotator.Net.Data
{

    public class SqlDataProvider : DataProvider
    {

        #region "Private Members"

        AvtSqlHelper_Table _TableSettings;

        #endregion

        #region "Constructors"

        public SqlDataProvider()
        {
            Init();
        }

        public override void Init()
        {
            _TableSettings = new AvtSqlHelper_Table(
                ConnStr,
                DbOwner + ObjQualifier + "avtRotator_Settings",
                false,
                new string[] { "ControlId", "SettingName" },
                "SettingValue"
            );
        }

        #endregion

        #region Settings

        public override void UpdateSetting(string controlId, string settingName, string settingValue)
        {
            _TableSettings.Update(new object[] { controlId, settingName }, settingValue);
        }

        public override IDataReader GetSettings(string controlId)
        {
            return _TableSettings.Get("Where ControlId=" + AvtSqlHelper_Table.EncodeSql(controlId));
        }

        public override IDataReader GetSetting(string controlId, string settingName)
        {
            return _TableSettings.Get("Where ControlId=" + AvtSqlHelper_Table.EncodeSql(controlId) + " AND SettingName=" + AvtSqlHelper_Table.EncodeSql(settingName));
        }

        public override void RemoveSettings(string controlId)
        {
            _TableSettings.Delete("ControlId=" + AvtSqlHelper_Table.EncodeSql(controlId));
        }


        public override void RemoveSetting(string controlId, string settingName)
        {
            _TableSettings.Delete(new object[] { controlId, settingName });
        }

        #endregion
    }
}
