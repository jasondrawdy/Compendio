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
using System.Security.Cryptography;

#endregion
namespace Compendio.Core.Data.Security
{
    /// <summary>
    /// Provides access to generating cryptographically strong random data.
    /// </summary>
    public class SecureRandom
    {
        #region Variables

        private const int BufferSize = 1024;  // must be a multiple of 4
        private byte[] RandomBuffer;
        private int BufferOffset;
        private RNGCryptoServiceProvider rng;

        #endregion
        #region Initialization

        /// <summary>
        /// Provides access to generating cryptographically strong random data.
        /// </summary>
        public SecureRandom()
        {
            RandomBuffer = new byte[BufferSize];
            rng = new RNGCryptoServiceProvider();
            BufferOffset = RandomBuffer.Length;
        }

        #endregion
        #region Methods

        private void FillBuffer()
        {
            rng.GetBytes(RandomBuffer);
            BufferOffset = 0;
        }
        /// <summary>
        /// Returns a non-negative cryptographically strong random integer.
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            if (BufferOffset >= RandomBuffer.Length)
            {
                FillBuffer();
            }
            int val = BitConverter.ToInt32(RandomBuffer, BufferOffset) & 0x7fffffff;
            BufferOffset += sizeof(int);
            return val;
        }
        /// <summary>
        /// Returns a non-negative cryptographically strong random integer that is less than the specified maximum value.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        public int Next(int maxValue)
        {
            return Next() % maxValue;
        }
        /// <summary>
        /// Returns an integer which is cryptographically strong and that is within a specified range.
        /// </summary>
        /// <param name="minValue">The incluse lower bound of the random number to be returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. Must be greater than or equal to minValue.</param>
        public int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new ArgumentOutOfRangeException("maxValue must be greater than or equal to minValue");
            }
            int range = maxValue - minValue;
            return minValue + Next(range);
        }
        /// <summary>
        /// Returns a cryptographically strong random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        public double NextDouble()
        {
            int val = Next();
            return (double)val / int.MaxValue;
        }
        /// <summary>
        /// Fills an array of bytes with a cryptographically strong sequence of random values.
        /// </summary>
        /// <param name="buff">The array to fill with a cryptographically strong sequence of random values.</param>
        public void GetBytes(byte[] buff)
        {
            rng.GetBytes(buff);
        }
        /// <summary>
        /// Returns an array filled with a cryptographically strong sequence of random values.
        /// </summary>
        /// <param name="size">The length of the array to return containing a cryptographically strong sequence of random values.</param>
        public byte[] GetRandomBytes(long size = BufferSize)
        {
            byte[] data = new byte[size];
            rng.GetBytes(data);
            return data;
        }

        #endregion
    }
}
