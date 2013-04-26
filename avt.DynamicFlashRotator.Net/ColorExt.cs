using System;
using System.Collections.Generic;
using System.Text;

namespace avt.DynamicFlashRotator.Net
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Useful C# snippets from CambiaResearch.com
    /// </summary>
    public static class ColorExt
    {
        #region -- Data Members --
        static char[] hexDigits = {
         '0', '1', '2', '3', '4', '5', '6', '7',
         '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        #endregion

        /// <summary>
        /// Convert a .NET Color to a hex string.
        /// </summary>
        /// <returns>ex: "FFFFFF", "AB12E9"</returns>
        public static string ColorToHexString(Color color)
        {
            if (color == Color.Transparent)
                return "transparent";

            byte[] bytes = new byte[3];
            bytes[0] = color.R;
            bytes[1] = color.G;
            bytes[2] = color.B;
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++) {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return "#" + new string(chars);
        }
    }

}
