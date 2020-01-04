/*
==============================================================================
Copyright © Jason Drawdy (CloneMerge)

All rights reserved.

The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

Except as contained in this notice, the name of the above copyright holder
shall not be used in advertising or otherwise to promote the sale, use or
other dealings in this Software without prior written authorization.
==============================================================================
*/

#region Imports

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Compendio.Core.Data.Security;
using System.Xml;
using System.Drawing;
using System.Security.Cryptography;

#endregion
namespace Compendio.Utils
{
    public static class Transformations
    {
        #region Methods

        #region Int

        /// <summary>
        /// Converts the provided string into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this string input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided bool into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this bool input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided double into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this double input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided long into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this long input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided float into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this float input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided decimal into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this decimal input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided string array into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this string[] input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided byte array into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this byte[] input)
        {
            return Convert.ToInt32(input);
        }
        /// <summary>
        /// Converts the provided integer array into its integer representation.
        /// </summary>
        /// <param name="input">The value to convert into an integer.</param>
        public static int ToInt(this int[] input)
        {
            return Convert.ToInt32(input);
        }

        #endregion
        #region Double

        /// <summary>
        /// Rounds a double precision floating point number considering the provided <see cref="MidpointRounding"/> object.
        /// </summary>
        /// <param name="value">The number which should be rounded using the provided format.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <param name="rounding">Specification for how to round the provided value if it is midway between two other numbers.</param>
        public static double Round(this double value, int precision, MidpointRounding rounding = MidpointRounding.AwayFromZero)
        {
            return Math.Round(value, precision, rounding);
        }

        #endregion
        #region Byte

        /// <summary>
        /// Converts the provided integer into a byte array.
        /// </summary>
        /// <param name="input">The value to convert into an array.</param>
        public static byte[] ToBytes(this int input)
        {
            return Encoding.UTF8.GetBytes(input.ToString());
        }
        /// <summary>
        /// Converts the provided string into a byte array.
        /// </summary>
        /// <param name="input">The value to convert into an array.</param>
        public static byte[] ToBytes(this string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        /// <summary>
        /// Converts a Base64 encoded string into a byte array.
        /// </summary>
        /// <param name="input">The encoded string that will be converted.</param>
        public static byte[] ToBytesFromBase64(this string input)
        {
            return Convert.FromBase64String(input);
        }
        /// <summary>
        /// Converts a byte array into a byte array which has been encoded using Base64.
        /// </summary>
        /// <param name="input">The original byte array to be encoded.</param>
        public static byte[] ToBase64(this byte[] input)
        {
            return Convert.ToBase64String(input).ToBytes();
        }
        /// <summary>
        /// Transforms a byte array into a cryptographically strong byte array using AES256 in CBC mode.
        /// </summary>
        /// <param name="input">The original plaintext byte array to be transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns></returns>
        public static byte[] EncryptBytes(this byte[] input, string password)
        {
            return Cryptography.AES.EncryptData(input, password.ToBytes());
        }
        /// <summary>
        /// Transforms a cryptographically strong byte array into its original plaintext array using AES256 in CBC mode.
        /// </summary>
        /// <param name="input">The ciphertext byte array to be transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns></returns>
        public static byte[] DecryptBytes(this byte[] input, string password)
        {
            return Cryptography.AES.DecryptData(input, password.ToBytes());
        }
        /// <summary>
        /// Combines two byte arrays into a single byte array.
        /// </summary>
        /// <param name="input">The first array to be merged.</param>
        /// <param name="partner">The second array to be merged.</param>
        /// <returns>An array which contains both the initial input and its partner.</returns>
        public static byte[] Merge(this byte[] input, byte[] partner)
        {
            byte[] merged = null;
            Buffer.BlockCopy(input, 0, merged, 0, input.Length);
            Buffer.BlockCopy(partner, 0, merged, merged.Length, partner.Length);
            return merged;
        }

        #endregion
        #region String

        /// <summary>
        /// Converts a byte array into its string representation.
        /// </summary>
        /// <param name="input">The byte array to be encoded into a string.</param>
        public static string GetString(this byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }
        /// <summary>
        /// Converts a byte array into its string representation which has been encoded with Base64.
        /// </summary>
        /// <param name="input">The byte array to be encoded into a string.</param>
        public static string GetBase64String(this byte[] input)
        {
            return Convert.ToBase64String(input);
        }
        /// <summary>
        /// Converts a long number into a string representing a file size.
        /// </summary>
        /// <param name="l">The number to be converted into a file size.</param>
        public static string ToFileSize(this long l)
        {
            return String.Format(new FileSizeFormatProvider(), "{0:fs}", l);
        }
        /// <summary>
        /// Splits a string into parts of equal length.
        /// </summary>
        /// <param name="s">The string which will be split.</param>
        /// <param name="partLength">The length that each part should be.</param>
        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
        /// <summary>
        /// Creates a list initialized with a given string.
        /// </summary>
        /// <param name="input">The first string which will be contained within the list.</param>
        public static List<string> CreateList(this string input)
        {
            List<string> list = new List<string>();
            list.Add(input);
            return list;
        }
        /// <summary>
        /// Reverses the provided string.
        /// </summary>
        /// <param name="input">The string to be reversed.</param>
        public static string ReverseString(this string input)
        {
            char[] myArray = input.ToCharArray();
            Array.Reverse(myArray);

            string output = string.Empty;

            foreach (char character in myArray)
                output += character;

            return output;
        }
        /// <summary>
        /// Transforms a string into a cryptographically strong ciphertext using AES256 in CBC mode.
        /// </summary>
        /// <param name="input">The string to be crypographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        public static string Encrypt(this string input, string password)
        {
            return Convert.ToBase64String(Cryptography.AES.EncryptData(input.ToBytes(), password.ToBytes()));
        }
        /// <summary>
        /// Transforms a cryptographically strong ciphertext into its original plaintext form using AES256 in CBC mode.
        /// </summary>
        /// <param name="input">The ciphertext to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        public static string Decrypt(this string input, string password)
        {
            return Encoding.UTF8.GetString(Cryptography.AES.DecryptData(Convert.FromBase64String(input), password.ToBytes()));
        }
        /// <summary>
        /// Converts a string into its Base64 equivalent.
        /// </summary>
        /// <param name="input">The string which should be encoded.</param>
        public static string Encode(this string input)
        {
            return Convert.ToBase64String(input.ToBytes());
        }
        /// <summary>
        /// Converts a Base64 string into its plaintext equivalent.
        /// </summary>
        /// <param name="input">The string which should be decoded.</param>
        public static string Decode(this string input)
        {
            return Convert.FromBase64String(input).GetString();
        }

        #endregion
        #region Color

        /// <summary>
        /// Converts RGB color from a string into its <see cref="Color"/> object representation.
        /// </summary>
        /// <param name="input">The value of the color to parse.</param>
        public static Color GetColorFromString(this string input)
        {
            Color myColor = new Color();
            var splitString = input.Split(',');
            var splitInt = splitString.Select(item => int.Parse(item)).ToArray();
            myColor = Color.FromArgb(splitInt[0], splitInt[1], splitInt[2]);
            return myColor;
        }

        #endregion
        #region XElement

        /// <summary>
        /// Extracts an <see cref="XElement"/> from an XML file given the element name.
        /// </summary>
        /// <param name="path">The location of the file to read from.</param>
        /// <param name="elementName">The name of the element which should be extracted from the path.</param>
        public static IEnumerable<XElement> GetElementFromName(this string path, string elementName)
        {
            using (XmlReader reader = XmlReader.Create(path))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == elementName)
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;
                            if (el != null)
                                yield return el;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to obtain a value from a specified XML element without throwing an exception.
        /// </summary>
        /// <param name="parentElement">The root element which contains the child node and its values.</param>
        /// <param name="elementName">The name of the child element to extract the provided value from.</param>
        /// <param name="defaultValue">The value which should be attempted to be extracted from the child element.</param>
        /// <returns>A value which was extracted from an XML child element nested within its parent.</returns>
        public static string TryGetElementValue(this XElement parentElement, string elementName, string defaultValue = null)
        {
            var foundElement = parentElement.Element(elementName);
            if (foundElement != null) { return foundElement.Value; }
            return defaultValue;
        }

        #endregion
        #region Data

        /// <summary>
        /// Transforms the current object into an array of bytes if serializable.
        /// </summary>
        /// <param name="obj">Any class which has been labeled with the <see cref="SerializableAttribute"/>.</param>
        /// <returns>A byte array for any serializable class object.</returns>
        public static byte[] Serialize(this object obj)
        {
            if (obj.GetType().IsSerializable)
            {
                try
                {
                    var formatter = new BinaryFormatter();
                    var stream = new MemoryStream();
                    formatter.Serialize(stream, obj);
                    byte[] data = stream.ToArray();
                    return data;
                }
                catch { throw new Exception("The provided object could not be serialized."); }
            }
            else
                throw new Exception("The provided object does not have a serialization attribute.");
        }

        /// <summary>
        /// Transforms an array of bytes to a generic class object.
        /// </summary>
        /// <param name="obj">The object's bytes to attempt to deserialize.</param>
        /// <returns>A generic class object which should be explicitly cast.</returns>
        public static object Deserialize(this byte[] obj)
        {
            try
            {
                var data = obj.ToArray();
                var stream = new MemoryStream();
                var formatter = new BinaryFormatter();
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                return formatter.Deserialize(stream);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        #endregion

        #endregion
    }
    #region Formatters

    /// <summary>
    /// Provides an interface which allows the formatting of a file size into its shorthand string representation.
    /// </summary>
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        #region Variables

        private const string fileSizeFormat = "fs";
        private const Decimal OneKiloByte = 1024M;
        private const Decimal OneMegaByte = OneKiloByte * 1024M;
        private const Decimal OneGigaByte = OneMegaByte * 1024M;
        private const Decimal OneTeraByte = OneGigaByte * 1024M;
        private const Decimal OnePetaByte = OneTeraByte * 1024M;

        #endregion
        #region Methods

        /// <summary>
        /// Obtains the formatting object type which will be used to actually format the value.
        /// </summary>
        /// <param name="formatType">The formatting object type to be used.</param>
        /// <returns></returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }

        /// <summary>
        /// Formats a file size into a shorthand string representation.
        /// </summary>
        /// <param name="format">The way the file size should be represented.</param>
        /// <param name="arg">The value to be formatted.</param>
        /// <param name="formatProvider">The provider which will handle the proceeding formatting.</param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (format == null || !format.StartsWith(fileSizeFormat))
                return defaultFormat(format, arg, formatProvider);

            if (arg is string)
                return defaultFormat(format, arg, formatProvider);

            Decimal size;
            try { size = Convert.ToDecimal(arg); }
            catch (InvalidCastException) { return defaultFormat(format, arg, formatProvider); }

            string suffix;
            if (size > OnePetaByte)
            {
                size /= OnePetaByte;
                suffix = " PB";
            }
            else if (size > OneTeraByte)
            {
                size /= OneTeraByte;
                suffix = " TB";
            }
            else if (size > OneGigaByte)
            {
                size /= OneGigaByte;
                suffix = " GB";
            }
            else if (size > OneMegaByte)
            {
                size /= OneMegaByte;
                suffix = " MB";
            }
            else if (size > OneKiloByte)
            {
                size /= OneKiloByte;
                suffix = " KB";
            }
            else
                suffix = " B";

            string precision = format.Substring(2);
            if (String.IsNullOrEmpty(precision)) precision = "2";
            return String.Format("{0:N" + precision + "}{1}", size, suffix);

        }

        private static string defaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            IFormattable formattableArg = arg as IFormattable;
            if (formattableArg != null) return formattableArg.ToString(format, formatProvider);
            return arg.ToString();
        }

        #endregion
    }

    #endregion
}
