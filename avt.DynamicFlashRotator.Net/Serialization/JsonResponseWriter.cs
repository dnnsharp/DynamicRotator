﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DnnSharp.DynamicRotator.Core.Serialization
{
    public class JsonResponseWriter : ResponseBuilder
    {
        StringBuilder _sbJson;
        int _cLevel = 0;
        Stack<bool> _inArray = new Stack<bool>();

        public JsonResponseWriter()
        {
            _sbJson = new StringBuilder();
        }

        public void BeginObject()
        {
            BeginObject(null);
        }

        public void BeginObject(string objName)
        {
            if (_cLevel > 0 && !_inArray.Peek()) { // don't write object name if this is the root object
                _sbJson.Append(SerializeString(objName));
                _sbJson.Append(":");
            }

            // start object
            _sbJson.Append("{");
            _cLevel++;
            _inArray.Push(false);
        }

        public void EndObject()
        {
            // check if we need to strip comma
            if (_sbJson[_sbJson.Length - 1] == ',')
                _sbJson.Remove(_sbJson.Length - 1, 1);

            // end object
            _sbJson.Append("}");
            _cLevel--;
            if (_cLevel > 0) {
                _sbJson.Append(",");
            }
            _inArray.Pop();
        }

        public void BeginArray()
        {
            BeginArray(null);
        }

        public void BeginArray(string objName)
        {
            if (_cLevel > 0 && !_inArray.Peek()) { // don't write object name if this is the root object
                _sbJson.Append(SerializeString(objName));
                _sbJson.Append(":");
            }

            // start object
            _sbJson.Append("[");
            _cLevel++;
            _inArray.Push(true);
        }

        public void EndArray()
        {
            // check if we need to strip comma
            if (_sbJson[_sbJson.Length - 1] == ',')
                _sbJson.Remove(_sbJson.Length - 1, 1);

            // end object
            _sbJson.Append("]");
            _cLevel--;
            if (_cLevel > 0) {
                _sbJson.Append(",");
            }
            _inArray.Pop();
        }

        public void QuickWriteObject(string objectName, string propName, object value)
        {
            BeginObject(objectName);
            WriteProperty(propName, value);
            EndObject();
        }

        public void QuickWriteObject(string objectName, string propName1, object value1, string propName2, object value2)
        {
            BeginObject(objectName);
            WriteProperty(propName1, value1);
            WriteProperty(propName2, value2);
            EndObject();
        }

        public void WriteProperty(string propName, object value)
        {
            if (_cLevel == 0) {
                throw new Exception("Start JSON with object or array!");
            }
            if (_inArray.Peek()) {
                _sbJson.AppendFormat("{0},", SerializeValue(value));
            } else {
                _sbJson.AppendFormat("{0}:{1},", SerializeString(propName), SerializeValue(value));
            }
        }

        public void WritePropertyLiteral(string propName, string strValue)
        {
            if (_cLevel == 0) {
                throw new Exception("Start JSON with object or array!");
            }
            if (_inArray.Peek()) {
                _sbJson.AppendFormat("{0},", strValue);
            } else {
                _sbJson.AppendFormat("{0}:{1},", SerializeString(propName), strValue);
            }
        }


        public override string ToString()
        {
            return _sbJson.ToString();
        }


        #region Helpers

        public static string SerializeValue(object value)
        {
            if (value is string)
                return SerializeString((string)value);
            if (IsNumeric(value))
                return SerializeNumber(Convert.ToDouble(value));
            if ((value is Boolean) && ((Boolean)value == true))
                return "true";
            if ((value is Boolean) && ((Boolean)value == false))
                return "false";
            if (value == null)
                return "null";

            return "\"\"";
        }

        public static string SerializeNumber(double number)
        {
            return Convert.ToString(number, CultureInfo.InvariantCulture);
        }

        public static string SerializeString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "\"\"";

            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);

            sb.Append('"');
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < charArray.Length; i++) {
                char c = charArray[i];
                if (c == '"') {
                    sb.Append("\\\"");
                } else if (c == '\\') {
                    sb.Append("\\\\");
                } else if (c == '/') {
                    sb.Append("\\/");
                } else if (c == '\b') {
                    sb.Append("\\b");
                } else if (c == '\f') {
                    sb.Append("\\f");
                } else if (c == '\n') {
                    sb.Append("\\n");
                } else if (c == '\r') {
                    sb.Append("\\r");
                } else if (c == '\t') {
                    sb.Append("\\t");
                } else {
                    int codepoint = Convert.ToInt32(c);
                    if ((codepoint >= 32) && (codepoint <= 126)) {
                        sb.Append(c);
                    } else {
                        sb.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                    }
                }
            }
            sb.Append('"');
            return sb.ToString();
        }

        public static bool IsNumeric(object o)
        {
            try {
                Double.Parse(o.ToString());
                return true;
            } catch (Exception) { }

            return false;
        }

        #endregion
    }
}
