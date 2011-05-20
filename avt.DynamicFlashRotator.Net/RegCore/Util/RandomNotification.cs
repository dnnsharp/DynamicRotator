using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace avt.DynamicFlashRotator.Net.RegCore.Util
{
    public class RandomNotification
    {
        static Random _RandomSeed = new Random();
        public const int DefaultFrequency = 40;
        int _Frequency = DefaultFrequency;

        public RandomNotification()
            :this(DefaultFrequency)
        {

        }

        public RandomNotification(int frequency)
        {
            _Frequency = frequency;
        }

        public bool IsTime()
        {
            return _RandomSeed.Next(0, _Frequency) == 1;
        }

        public string Render(IList<string> keywords, params object[] args)
        {
            HttpRequest request = HttpContext.Current.Request;
            string keyword = keywords[Math.Abs(request.RawUrl.GetHashCode() % keywords.Count)];
            bool isIp = Regex.IsMatch(request.Url.Host, ".*\\d+\\.\\d+\\.\\d+\\.\\d+.*");
            if (isIp) {
                keyword = Regex.Replace(keyword, "<a ", "<a rel='nofollow' ");
            }

            return string.Format(keyword, args);
        }
    }
}
