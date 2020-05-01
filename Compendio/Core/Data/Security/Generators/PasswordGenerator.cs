/*
==============================================================================
Copyright © Jason Drawdy 

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
using System.Linq;
using System.Security.Cryptography;

#endregion
namespace Compendio.Core.Data.Security
{
    /// <summary>
    /// Allows the creation of cryptographically secure passwords.
    /// </summary>
    public static class PasswordGenerator
    {
        #region Variables

        /// <summary>
        /// Common letter, number, and symbol combination types.
        /// </summary>
        public enum PasswordType
        {
            AlphaMixed,
            AlphaUpper,
            AlphaLower,
            Mixed,
            Uppercase,
            Lowercase,
            Numbers
        }

        // Lists of characters.
        private static string[] uppercase = { "A", "B", "C", "D", "E", "F",
                                              "G", "H", "I", "J", "K", "L",
                                              "M", "N", "O", "P", "Q", "R",
                                              "S", "T", "U", "V", "W", "X",
                                              "Y", "Z"};

        private static string[] lowercase = { "a", "b", "c", "d", "e", "f",
                                              "g", "h", "i", "j", "k", "l",
                                              "m", "n", "o", "p", "q", "r",
                                              "s", "t", "u", "v", "w", "x",
                                              "y", "z" };

        private static string[] nonalpha = { "!", "@", "#", "$", "%",
                                             "^", "&", "*", "(", ")",
                                             "-", "_", "=", "+", ";",
                                             ":", "[", "]", "{", "}",
                                             "<", ">", ",", ".", "|"};

        private static string[] numbers = { "0", "1", "2", "3", "4",
                                            "5", "6", "7", "8", "9"};

        // The random number provider.
        private static SecureRandom random = new SecureRandom();

        #endregion
        #region Methods

        // Return a random integer between a min and max value.
        private static int RandomInteger(int min, int max)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] data = random.GetRandomBytes(4);

                // Convert our bytes into an unsigned integer.
                scale = BitConverter.ToUInt32(data, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
        }

        /// <summary>
        /// Creates a cryptographically secure password provided a <see cref="PasswordType"/>, length, and whether or not to include non-alphanumerical symbols.
        /// </summary>
        /// <param name="type">The combination type to use when generation a password.</param>
        /// <param name="length">The length the generated password should be.</param>
        /// <param name="nonAlphaNumerics">A flag specifying whether or not to include non-alphanumerical symbols.</param>
        /// <returns>A cryptographically secure password represented as a string.</returns>
        public static string GeneratePassword(PasswordType type, int length, bool nonAlphaNumerics)
        {
            string password = "";
            Random random = new Random();

            // Concatenate our arrays.
            string[] alphamixed, alphaupper, alphalower, mixed, upper, lower, n;
            if (nonAlphaNumerics == true)
            {
                alphamixed = uppercase.Concat(lowercase).Concat(nonalpha).Concat(numbers).ToArray();
                alphaupper = uppercase.Concat(nonalpha).Concat(numbers).ToArray();
                alphalower = lowercase.Concat(nonalpha).Concat(numbers).ToArray();
                mixed = uppercase.Concat(lowercase).Concat(nonalpha).ToArray();
                upper = uppercase.Concat(nonalpha).ToArray();
                lower = lowercase.Concat(nonalpha).ToArray();
                n = numbers.Concat(nonalpha).ToArray(); //numbers
            }
            else
            {
                alphamixed = uppercase.Concat(lowercase).Concat(numbers).ToArray();
                alphaupper = uppercase.Concat(numbers).ToArray();
                alphalower = lowercase.Concat(numbers).ToArray();
                mixed = uppercase.Concat(lowercase).ToArray();
                upper = uppercase.ToArray();
                lower = lowercase.ToArray();
                n = numbers.ToArray(); //numbers
            }

            switch (type)
            {
                case PasswordType.AlphaMixed:
                    for (int i = 0; i < length; i++)
                        password += alphamixed[RandomInteger(0, alphamixed.Length)];
                    break;
                case PasswordType.AlphaUpper:
                    for (int i = 0; i < length; i++)
                        password += alphaupper[RandomInteger(0, alphaupper.Length)];
                    break;
                case PasswordType.AlphaLower:
                    for (int i = 0; i < length; i++)
                        password += alphalower[RandomInteger(0, alphalower.Length)];
                    break;
                case PasswordType.Mixed:
                    for (int i = 0; i < length; i++)
                        password += mixed[RandomInteger(0, mixed.Length)];
                    break;
                case PasswordType.Uppercase:
                    for (int i = 0; i < length; i++)
                        password += upper[RandomInteger(0, upper.Length)];
                    break;
                case PasswordType.Lowercase:
                    for (int i = 0; i < length; i++)
                        password += lower[RandomInteger(0, lower.Length)];
                    break;
                case PasswordType.Numbers:
                    for (int i = 0; i < length; i++)
                        password += n[RandomInteger(0, n.Length)];
                    break;
                default:
                    break;
            }

            return (password);
        }

        #endregion
    }
}
