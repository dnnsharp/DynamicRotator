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
        AvtSqlHelper_Table _TableSlides;
        AvtSqlHelper_Table _TableSlideObjects;

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

            _TableSlides = new AvtSqlHelper_Table(
                ConnStr,
                DbOwner + ObjQualifier + "avtRotator_Slides",
                true,
                new string[] { "SlideId" },
                "ControlId", "Title", "DurationSeconds", "BackgroundGradientFrom", "BackgroundGradientTo",
                "Link_Url", "Link_Caption", "Link_Target", "Link_UseTextsBackground",
                "Mp3_Url", "Mp3_ShowPlayer", "Mp3_IconColor"
            );

            _TableSlideObjects = new AvtSqlHelper_Table(
                ConnStr,
                DbOwner + ObjQualifier + "avtRotator_SlideObjects",
                true,
                new string[] { "ObjectId" },
                "SlideId", "ObjectType", "Name"
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


        #region Slides

        public override int UpdateSlide(
            int slideId, string controlId, string title, int durationSeconds, string backgroundGradientFrom, string backgroundGradientTo,
            string linkUrl, string linkCaption, string linkTarget, bool useTextsBk,
            string mp3LinkUrl, bool mp3ShowPlayer, string mp3IconColor)
        {
            return _TableSlides.Update(
                new object[] { slideId }, controlId, title, durationSeconds, backgroundGradientFrom, backgroundGradientTo,
                linkUrl, linkCaption, linkTarget, useTextsBk,
                mp3LinkUrl, mp3ShowPlayer, mp3IconColor
            );
        }

        public override IDataReader GetSlides(string controlId)
        {
            return _TableSlides.Get("Where ControlID=" + AvtSqlHelper_Table.EncodeSql(controlId));
        }

        public override IDataReader GetSlide(int slideId)
        {
            return _TableSlides.Get("Where SlideId=" + slideId);
        }

        public override void RemoveSlide(int slideId)
        {
            _TableSlides.Delete(new object[] { slideId });
        }



        public override int UpdateSlideObject(
           int slideObjectId, int slideId, string objectType, string Name)
        {
            return _TableSlideObjects.Update(
                new object[] { slideObjectId }, slideId, objectType, Name
            );
        }

        public override IDataReader GetSlideObjects(int slideId)
        {
            return _TableSlideObjects.Get("Where SideId=" + slideId);
        }

        public override IDataReader GetSlideObject(int slideObjectId)
        {
            return _TableSlideObjects.Get("Where ObjectId=" + slideObjectId);
        }

        public override void RemoveSlideObject(int slideObjectId)
        {
            _TableSlideObjects.Delete(new object[] { slideObjectId });
        }

        #endregion

    }
}
