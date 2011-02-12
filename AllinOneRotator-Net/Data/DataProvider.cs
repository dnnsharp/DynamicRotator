
using System.Data;
using System.Data.SqlTypes;

namespace avt.AllinOneRotator.Net.Data
{
    public abstract class DataProvider
    {

        #region "Shared/Static Methods"

        // singleton reference to the instantiated object 
        private static DataProvider objProvider = null;

        // constructor
        static DataProvider()
        {
            CreateProvider();
        }

        // dynamically create provider
        private static void CreateProvider()
        {
            objProvider = new SqlDataProvider(); // RedirectEngine.Configuration.GetDataProvider();

            //objProvider = (DataProvider)Reflection.CreateObject("data", "avt.RedirectToolkit", "");
        }

        // return the provider
        public static DataProvider Instance()
        {
            return objProvider;
        }

        public abstract void Init();

        #endregion

        
        #region Configuration

        string _ConnStr = "";
        public string ConnStr { get { return _ConnStr; } set { _ConnStr = value; } }

        string _DbOwner = "";
        public string DbOwner { 
            get { return _DbOwner; } 
            set { 
                _DbOwner = value;
                if (!string.IsNullOrEmpty(_DbOwner) && _DbOwner.IndexOf('.') != _DbOwner.Length - 1) {
                    _DbOwner += ".";
                }
            } 
        }

        string _ObjQualifier = "";
        public string ObjQualifier { get { return _ObjQualifier; } set { _ObjQualifier = value; } }

        #endregion


        // Settings
        public abstract void UpdateSetting(string controlId, string settingName, string settingValue);
        public abstract IDataReader GetSettings(string controlId);
        public abstract IDataReader GetSetting(string controlId, string settingName);
        public abstract void RemoveSettings(string controlId);
        public abstract void RemoveSetting(string controlId, string settingName);

        // Slides
        public abstract void UpdateSlide(
            int slideId, string controlId, string title, int durationSeconds, string backgroundGradientFrom, string backgroundGradientTo,
            string linkUrl, string linkCaption, string linkTarget, bool useTextsBk,
            string mp3LinkUrl, bool mp3ShowPlayer, string mp3IconColor
        );
        public abstract IDataReader GetSlides(string controlId);

        public abstract void RemoveSlide(int slideId);
    }
}
