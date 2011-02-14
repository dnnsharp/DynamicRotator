
using System.Data;
using System.Data.SqlTypes;
using avt.AllinOneRotator.Net.Services;

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

        public abstract void Init(IConfiguration config);

        #endregion


        // Settings
        public abstract void UpdateSetting(string controlId, string settingName, string settingValue);
        public abstract IDataReader GetSettings(string controlId);
        public abstract IDataReader GetSetting(string controlId, string settingName);
        public abstract void RemoveSettings(string controlId);
        public abstract void RemoveSetting(string controlId, string settingName);

        // Slides
        public abstract int UpdateSlide(
            int slideId, string controlId, string title, int durationSeconds, string backgroundGradientFrom, string backgroundGradientTo,
            string linkUrl, string linkCaption, string linkTarget, bool useTextsBk,
            string mp3LinkUrl, bool mp3ShowPlayer, string mp3IconColor
        );
        public abstract IDataReader GetSlides(string controlId);
        public abstract IDataReader GetSlide(int slideId);
        public abstract void RemoveSlide(int slideId);

        // Slide Objects
        public abstract int UpdateSlideObject(
            int slideObjectId, int slideId, string objectType, string Name
        );
        public abstract IDataReader GetSlideObjects(int slideId);
        public abstract IDataReader GetSlideObject(int slideObjectId);
        public abstract void RemoveSlideObject(int slideObjectId);
    }
}
