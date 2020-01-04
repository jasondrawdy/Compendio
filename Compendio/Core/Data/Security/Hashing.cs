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
using System.Security.Cryptography;
using System.Text;

#endregion
namespace Compendio.Core.Data.Security
{
    /// <summary>
    /// Provides access to common cryptographically strong hashing methods.
    /// </summary>
    public static class Hashing
    {
        #region Variables

        /// <summary>
        /// Common hashing algorithms used in most security scenarios.
        /// </summary>
        public enum HashType
        {
            MD5,
            RIPEMD160,
            SHA1,
            SHA256,
            SHA384,
            SHA512
        }

        #endregion
        #region Methods

        private static byte[] GetHash(string input, HashType hash)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            switch (hash)
            {
                case HashType.MD5:
                    return MD5.Create().ComputeHash(inputBytes);
                case HashType.RIPEMD160:
                    return RIPEMD160.Create().ComputeHash(inputBytes);
                case HashType.SHA1:
                    return SHA1.Create().ComputeHash(inputBytes);
                case HashType.SHA256:
                    return SHA256.Create().ComputeHash(inputBytes);
                case HashType.SHA384:
                    return SHA384.Create().ComputeHash(inputBytes);
                case HashType.SHA512:
                    return SHA512.Create().ComputeHash(inputBytes);
                default:
                    return inputBytes;
            }

        }

        private static byte[] GetFileHash(string path, HashType hash)
        {
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, (1024 * 1024)))
            {
                switch (hash)
                {
                    case HashType.MD5:
                        return MD5.Create().ComputeHash(file);
                    case HashType.RIPEMD160:
                        return RIPEMD160.Create().ComputeHash(file);
                    case HashType.SHA1:
                        return SHA1.Create().ComputeHash(file);
                    case HashType.SHA256:
                        return SHA256.Create().ComputeHash(file);
                    case HashType.SHA384:
                        return SHA384.Create().ComputeHash(file);
                    case HashType.SHA512:
                        return SHA512.Create().ComputeHash(file);
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Calculates the hash of the provided string.
        /// </summary>
        /// <param name="input">The string to be transformed into a cryptographic hash.</param>
        /// <param name="type">The hashing algorithm used to transform the provided data.</param>
        public static string ComputeMessageHash(string input, HashType type)
        {
            try
            {
                byte[] hash = GetHash(input, type);
                StringBuilder result = new StringBuilder();

                for (int i = 0; i <= hash.Length - 1; i++)
                    result.Append(hash[i].ToString("x2"));

                return result.ToString();
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Calculates the hash of a file given a valid path.
        /// </summary>
        /// <param name="input">The location of the file to be transformed.</param>
        /// <param name="type">The algorithm used to transform the input file.</param>
        /// <returns></returns>
        public static string ComputeFileHash(string input, HashType type)
        {
            try
            {
                byte[] hash = GetFileHash(input, type);
                StringBuilder result = new StringBuilder();

                for (int i = 0; i <= hash.Length - 1; i++)
                    result.Append(hash[i].ToString("x2"));

                return result.ToString();
            }
            catch { return string.Empty; }
        }

        #endregion
    }
}
