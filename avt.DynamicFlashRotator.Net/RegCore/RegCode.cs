using System;
using System.Collections.Generic;
using System.Web;

namespace avt.DynamicFlashRotator.Net.RegCore
{
    public class RegCode
    {
        static Random rGen = new Random();

        string _RegCode;
        string _prodCode;
        string _variantCode;
        string _hashCheck;
        string _custPart;
        string _randPart;

        DateTime _dateExpire = DateTime.MinValue;

        public bool HasTimeBomb
        {
            get { return _dateExpire != DateTime.MinValue; }
        }

        public string ProductCode
        {
            get { return _prodCode; }
        }

        public string VariantCode
        {
            get { return _variantCode; }
        }

        public DateTime DateExpire
        {
            get { return _dateExpire; }
        }

        public string R
        {
            get { return _randPart; }
        }

        public bool IsExpired()
        {
            return HasTimeBomb && _dateExpire < DateTime.Now;
        }

        public RegCode(string regCode)
        {
            _RegCode = regCode;

            // parse parts
            string[] parts = regCode.Split('-');
            int iPart = 0;
            _prodCode = parts[iPart++];
            _variantCode = parts[iPart++];
            if (parts.Length == 4) { // has timebomb
                DateTime centuryBegin = new DateTime(2001, 1, 1);
                _dateExpire = centuryBegin.AddDays(Convert.ToInt32(parts[iPart++]));
            }

            if (parts.Length < 3)
                throw new FormatException("Invalid Registration Code Format");

            _hashCheck = parts[iPart].Substring(0, 20);
            _custPart = parts[iPart].Substring(20);
            _randPart = parts[iPart].Substring(28);

            //HttpContext.Current.Response.Write(_hashCheck+"<Br />");
            //HttpContext.Current.Response.Write(_custPart + "<Br />");
            //HttpContext.Current.Response.Write(_randPart + "<Br />");

            // validate length
            if (_randPart.Length != 6)
                throw new FormatException("Invalid Registration Code Format");
        }

        public bool IsTrial {
            get {
                return VariantCode == "30DAY" || VariantCode == "14DAY" || VariantCode == "90DAY";
            }
        }

        private RegCode() // private constructor called via Generate
        {
        }


        public override string ToString()
        {
            return _RegCode;
        }
    }
}