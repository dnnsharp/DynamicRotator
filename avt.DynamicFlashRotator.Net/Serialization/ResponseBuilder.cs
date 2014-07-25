using System;
using System.Collections.Generic;
using System.Text;

namespace DnnSharp.DynamicRotator.Core.Serialization
{
    public interface ResponseBuilder
    {
        void BeginObject();
        void BeginObject(string objName);
        void EndObject();

        void BeginArray();
        void BeginArray(string objName);
        void EndArray();

        void WriteProperty(string propName, object value);
        void WritePropertyLiteral(string propName, string strValue);

        void QuickWriteObject(string objectName, string propName, object value);
        void QuickWriteObject(string objectName, string propName1, object value1, string propName2, object value2);

    }
}
