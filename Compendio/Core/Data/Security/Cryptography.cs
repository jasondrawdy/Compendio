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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Compendio.Core.Data.IO;
using Compendio.Utils;
using SecurityDriven.Inferno;
using LongFile = Pri.LongPath.File;


#endregion
namespace Compendio.Core.Data.Security
{
    /// <summary>
    /// Provides access to cryptographic methods to transform strings, arrays, and entire files.
    /// </summary>
    public static class Cryptography
    {
        /// <summary>
        /// Contains methods for manipulating data using the AES encryption algorithm.
        /// </summary>
        public static class AES
        {
            #region Methods

        /// <summary>
        /// Transforms a file into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be cryptographically transformed.</param>
        /// <param name="encryptedFile">The path where the new file will be created.</param>
        /// <param name="password">The key to be used during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful.</returns>
        public static bool EncryptFile(string originalFile, string encryptedFile, string password)
        {
            try
            {
                string originalFilename = originalFile;
                string encryptedFilename = encryptedFile;

                using (var originalStream = LongFile.Open(originalFile, FileMode.Open))
                using (var encryptedStream = LongFile.Create(encryptedFile, 8192))
                using (var encTransform = new EtM_EncryptTransform(key: password.ToBytes()))
                using (var crypoStream = new CryptoStream(encryptedStream, encTransform, CryptoStreamMode.Write))
                {
                    originalStream.CopyTo(crypoStream);
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Transforms a file into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be cryptographically transformed.</param>
        /// <param name="encryptedFile">The path where the new file will be created.</param>
        /// <param name="password">The key to be used during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful.</returns>
        public static bool EncryptFile(string originalFile, string encryptedFile, byte[] password)
        {
            try
            {
                string originalFilename = originalFile;
                string encryptedFilename = encryptedFile;

                using (var originalStream = LongFile.Open(originalFile, FileMode.Open))
                using (var encryptedStream = LongFile.Create(encryptedFile, 8192))
                using (var encTransform = new EtM_EncryptTransform(key: password))
                using (var crypoStream = new CryptoStream(encryptedStream, encTransform, CryptoStreamMode.Write))
                {
                    originalStream.CopyTo(crypoStream);
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Transforms a cryptographic file into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be transformed into its decrypted form.</param>
        /// <param name="decryptedFile">The location the decrypted file will be written to.</param>
        /// <param name="password">The key to use during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful or not.</returns>
        public static bool DecryptFile(string originalFile, string decryptedFile, string password)
        {
            try
            {
                string originalFilename = originalFile;
                string decryptedFilename = decryptedFile;

                using (var encryptedStream = LongFile.Open(originalFilename, FileMode.Open))
                using (var decryptedStream = LongFile.Create(decryptedFilename, 8192))
                using (var decTransform = new EtM_DecryptTransform(key: password.ToBytes()))
                {
                    using (var cryptoStream = new CryptoStream(encryptedStream, decTransform, CryptoStreamMode.Read))
                        cryptoStream.CopyTo(decryptedStream);

                    if (!decTransform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Transforms a cryptographic file into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be transformed into its decrypted form.</param>
        /// <param name="decryptedFile">The location the decrypted file will be written to.</param>
        /// <param name="password">The key to use during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful or not.</returns>
        public static bool DecryptFile(string originalFile, string decryptedFile, byte[] password)
        {
            try
            {
                string originalFilename = originalFile;
                string decryptedFilename = decryptedFile;

                using (var encryptedStream = LongFile.Open(originalFilename, FileMode.Open))
                using (var decryptedStream = LongFile.Create(decryptedFilename, 8192))
                using (var decTransform = new EtM_DecryptTransform(key: password))
                {
                    using (var cryptoStream = new CryptoStream(encryptedStream, decTransform, CryptoStreamMode.Read))
                        cryptoStream.CopyTo(decryptedStream);

                    if (!decTransform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Asynchronously transforms a file into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be cryptographically transformed.</param>
        /// <param name="encryptedFile">The path where the new file will be created.</param>
        /// <param name="password">The key to be used during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful.</returns>
        public static async Task<bool> EncryptFileAsync(string originalFile, string encryptedFile, string password)
        {
            try
            {
                string originalFilename = originalFile;
                string encryptedFilename = encryptedFile;

                using (var originalStream = LongFile.Open(originalFile, FileMode.Open))
                using (var encryptedStream = LongFile.Create(encryptedFile, 8192))
                using (var encTransform = new EtM_EncryptTransform(key: password.ToBytes()))
                using (var crypoStream = new CryptoStream(encryptedStream, encTransform, CryptoStreamMode.Write))
                {
                    await originalStream.CopyToAsync(crypoStream);
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Asynchronously transforms a file into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be cryptographically transformed.</param>
        /// <param name="encryptedFile">The path where the new file will be created.</param>
        /// <param name="password">The key to be used during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful.</returns>
        public static async Task<bool> EncryptFileAsync(string originalFile, string encryptedFile, byte[] password)
        {
            try
            {
                string originalFilename = originalFile;
                string encryptedFilename = encryptedFile;

                using (var originalStream = LongFile.Open(originalFile, FileMode.Open))
                using (var encryptedStream = LongFile.Create(encryptedFile, 8192))
                using (var encTransform = new EtM_EncryptTransform(key: password))
                using (var crypoStream = new CryptoStream(encryptedStream, encTransform, CryptoStreamMode.Write))
                {
                    await originalStream.CopyToAsync(crypoStream);
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// Asynchronously transforms a cryptographic file into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be transformed into its decrypted form.</param>
        /// <param name="decryptedFile">The location the decrypted file will be written to.</param>
        /// <param name="password">The key to use during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful or not.</returns>
        public static async Task<bool> DecryptFileAsync(string originalFile, string decryptedFile, string password)
        {
            try
            {
                string originalFilename = originalFile;
                string decryptedFilename = decryptedFile;

                using (var encryptedStream = LongFile.Open(originalFilename, FileMode.Open))
                using (var decryptedStream = LongFile.Create(decryptedFilename, 8192))
                using (var decTransform = new EtM_DecryptTransform(key: password.ToBytes()))
                {
                    using (var cryptoStream = new CryptoStream(encryptedStream, decTransform, CryptoStreamMode.Read))
                        await cryptoStream.CopyToAsync(decryptedStream);

                    if (!decTransform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Asynchronously transforms a cryptographic file into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="originalFile">The path to the file to be transformed into its decrypted form.</param>
        /// <param name="decryptedFile">The location the decrypted file will be written to.</param>
        /// <param name="password">The key to use during the cryptographic transformation.</param>
        /// <returns>A flag determining whether or not the operation was successful or not.</returns>
        public static async Task<bool> DecryptFileAsync(string originalFile, string decryptedFile, byte[] password)
        {
            try
            {
                string originalFilename = originalFile;
                string decryptedFilename = decryptedFile;

                using (var encryptedStream = LongFile.Open(originalFilename, FileMode.Open))
                using (var decryptedStream = LongFile.Create(decryptedFilename, 8192))
                using (var decTransform = new EtM_DecryptTransform(key: password))
                {
                    using (var cryptoStream = new CryptoStream(encryptedStream, decTransform, CryptoStreamMode.Read))
                        await cryptoStream.CopyToAsync(decryptedStream);

                    if (!decTransform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Transforms an array of bytes into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The data which will be transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the cryptographically strong equivalent of the original data.</returns>
        public static byte[] EncryptData(byte[] data, string password)
        {
            using (var original = new MemoryStream(data))
            using (var encrypted = new MemoryStream())
            using (var transform = new EtM_EncryptTransform(key: password.ToBytes()))
            using (var crypto = new CryptoStream(encrypted, transform, CryptoStreamMode.Write))
            {
                original.CopyTo(encrypted);
                return encrypted.ToArray();
            }
        }

        /// <summary>
        /// Transforms an array of bytes into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The data which will be transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the cryptographically strong equivalent of the original data.</returns>
        public static byte[] EncryptData(byte[] data, byte[] password)
        {
            using (var original = new MemoryStream(data))
            using (var encrypted = new MemoryStream())
            using (var transform = new EtM_EncryptTransform(key: password))
            using (var crypto = new CryptoStream(encrypted, transform, CryptoStreamMode.Write))
            {
                original.CopyTo(encrypted);
                return encrypted.ToArray();
            }
        }

        /// <summary>
        /// Transforms an array of cryptographic data into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The encrypted data to transform into its original form.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the original plaintext data.</returns>
        public static byte[] DecryptData(byte[] data, string password)
        {
            using (var original = new MemoryStream(data))
            using (var decrypted = new MemoryStream())
            using (var transform = new EtM_DecryptTransform(key: password.ToBytes()))
            {
                using (var crypto = new CryptoStream(original, transform, CryptoStreamMode.Read))
                    crypto.CopyTo(decrypted);
                if (!transform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                return decrypted.ToArray();
            }
        }

        /// <summary>
        /// Transforms an array of cryptographic data into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The encrypted data to transform into its original form.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the original plaintext data.</returns>
        public static byte[] DecryptData(byte[] data, byte[] password)
        {
            using (var original = new MemoryStream(data))
            using (var decrypted = new MemoryStream())
            using (var transform = new EtM_DecryptTransform(key: password))
            {
                using (var crypto = new CryptoStream(original, transform, CryptoStreamMode.Read))
                    crypto.CopyTo(decrypted);
                if (!transform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                return decrypted.ToArray();
            }
        }

        /// <summary>
        /// Asynchronously transforms an array of bytes into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The data which will be transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the cryptographically strong equivalent of the original data.</returns>
        public static async Task<byte[]> EncryptDataAsync(byte[] data, string password)
        {
            using (var original = new MemoryStream(data))
            using (var encrypted = new MemoryStream())
            using (var transform = new EtM_EncryptTransform(key: password.ToBytes()))
            using (var crypto = new CryptoStream(encrypted, transform, CryptoStreamMode.Write))
            {
                await original.CopyToAsync(encrypted);
                return encrypted.ToArray();
            }
        }

        /// <summary>
        /// Asynchronously transforms an array of bytes into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The data which will be transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the cryptographically strong equivalent of the original data.</returns>
        public static async Task<byte[]> EncryptDataAsync(byte[] data, byte[] password)
        {
            using (var original = new MemoryStream(data))
            using (var encrypted = new MemoryStream())
            using (var transform = new EtM_EncryptTransform(key: password))
            using (var crypto = new CryptoStream(encrypted, transform, CryptoStreamMode.Write))
            {
                await original.CopyToAsync(encrypted);
                return encrypted.ToArray();
            }
        }

        /// <summary>
        /// Asynchronously transforms an array of cryptographic data into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The encrypted data to transform into its original form.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the original plaintext data.</returns>
        public static async Task<byte[]> DecryptDataAsync(byte[] data, string password)
        {
            using (var original = new MemoryStream(data))
            using (var decrypted = new MemoryStream())
            using (var transform = new EtM_DecryptTransform(key: password.ToBytes()))
            {
                using (var crypto = new CryptoStream(original, transform, CryptoStreamMode.Read))
                    await crypto.CopyToAsync(decrypted);
                if (!transform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                return decrypted.ToArray();
            }
        }

        /// <summary>
        /// Asynchronously transforms an array of cryptographic data into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The encrypted data to transform into its original form.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <returns>An array containing the original plaintext data.</returns>
        public static async Task<byte[]> DecryptDataAsync(byte[] data, byte[] password)
        {
            using (var original = new MemoryStream(data))
            using (var decrypted = new MemoryStream())
            using (var transform = new EtM_DecryptTransform(key: password))
            {
                using (var crypto = new CryptoStream(original, transform, CryptoStreamMode.Read))
                    await crypto.CopyToAsync(decrypted);
                if (!transform.IsComplete) { throw new Exception("Not all blocks have been decrypted."); }
                return decrypted.ToArray();
            }
        }

        /// <summary>
        /// Transforms a plaintext string into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into a ciphertext.</returns>
        public static string Encrypt(string data, string password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = EncryptData(plaintext, password.ToBytes());
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Transforms a plaintext string into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into a ciphertext.</returns>
        public static string Encrypt(string data, byte[] password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = EncryptData(plaintext, password);
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Transforms a ciphertext string into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into plaintext.</returns>
        public static string Decrypt(string data, string password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = DecryptData(plaintext, password.ToBytes());
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Transforms a ciphertext string into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into plaintext.</returns>
        public static string Decrypt(string data, byte[] password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = DecryptData(plaintext, password);
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Asynchronously transforms a plaintext string into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into a ciphertext.</returns>
        public static async Task<string> EncryptAsync(string data, string password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = await EncryptDataAsync(plaintext, password);
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Asynchronously transforms a plaintext string into its cryptographically strong equivalent using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into a ciphertext.</returns>
        public static async Task<string> EncryptAsync(string data, byte[] password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = await EncryptDataAsync(plaintext, password);
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Asynchronously transforms a ciphertext string into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into plaintext.</returns>
        public static async Task<string> DecryptAsync(string data, string password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = await DecryptDataAsync(plaintext, password);
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        /// <summary>
        /// Asynchronously transforms a ciphertext string into its original form using AES256 in CBC mode.
        /// </summary>
        /// <param name="data">The string to be cryptographically transformed.</param>
        /// <param name="password">The key to use during the transformation.</param>
        /// <param name="encode">Allows the returned value to be encoded into Base64 or not.</param>
        /// <returns>A string that has been cryptographically transformed into plaintext.</returns>
        public static async Task<string> DecryptAsync(string data, byte[] password, bool encode = false)
        {
            byte[] plaintext = data.ToBytes();
            byte[] ciphertext = await DecryptDataAsync(plaintext, password);
            return (encode) ? ciphertext.GetBase64String() : ciphertext.GetString();
        }

        #endregion
        }
        /// <summary>
        /// Contains methods for use regarding the RSA encryption algorithm.
        /// </summary>
        public static class RSA
        {
            #region Methods

            /// <summary>
            /// Holds information about an RSA private key formatted with the PEM algorithm.
            /// </summary>
            public class PEMProvider
            {
                /// <summary>
                /// Contains the converted keypair of a <see cref="RSACryptoServiceProvider"/>.
                /// </summary>
                public string Key { get; private set; }

                /// <summary>
                /// Holds information about a converted <see cref="RSACryptoServiceProvider"/> into PEM format.
                /// </summary>
                /// <param name="csp">The crypto service provider containing the keypair to be converted.</param>
                public PEMProvider(RSACryptoServiceProvider csp)
                {
                    if (csp.PublicOnly)
                        Key = ExportPublicKey(csp);
                    else
                        Key = ExportPrivateKey(csp);
                }

                /// <summary>
                /// Exports an XML formatted private key to its PEM equivalent.
                /// </summary>
                /// <param name="csp">The crypto service provider containing a private key to convert into the PEM format.</param>
                public static string ExportPrivateKey(RSACryptoServiceProvider csp)
                {
                    if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
                    using (TextWriter output = new StreamWriter(new MemoryStream()))
                    {
                        var parameters = csp.ExportParameters(true);
                        using (var stream = new MemoryStream())
                        {
                            var writer = new BinaryWriter(stream);
                            writer.Write((byte)0x30); // SEQUENCE
                            using (var innerStream = new MemoryStream())
                            {
                                var innerWriter = new BinaryWriter(innerStream);
                                EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                                EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                                EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                                EncodeIntegerBigEndian(innerWriter, parameters.D);
                                EncodeIntegerBigEndian(innerWriter, parameters.P);
                                EncodeIntegerBigEndian(innerWriter, parameters.Q);
                                EncodeIntegerBigEndian(innerWriter, parameters.DP);
                                EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                                EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
                                var length = (int)innerStream.Length;
                                EncodeLength(writer, length);
                                writer.Write(innerStream.GetBuffer(), 0, length);
                            }

                            var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                            output.WriteLine("-----BEGIN RSA PRIVATE KEY-----");
                            // Output as Base64 with lines chopped at 64 characters
                            for (var i = 0; i < base64.Length; i += 64)
                            {
                                output.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                            }
                            output.WriteLine("-----END RSA PRIVATE KEY-----");
                        }
                        return output.ToString();
                    }
                }

                /// <summary>
                /// Exports an XML formatted public key to its PEM equivalent.
                /// </summary>
                /// <param name="csp">The crypto service provider containing a public key to convert into the PEM format.</param>
                public static string ExportPublicKey(RSACryptoServiceProvider csp)
                {
                    using (TextWriter output = new StreamWriter(new MemoryStream()))
                    {
                        var parameters = csp.ExportParameters(false);
                        using (var stream = new MemoryStream())
                        {
                            var writer = new BinaryWriter(stream);
                            writer.Write((byte)0x30); // SEQUENCE
                            using (var innerStream = new MemoryStream())
                            {
                                var innerWriter = new BinaryWriter(innerStream);
                                innerWriter.Write((byte)0x30); // SEQUENCE
                                EncodeLength(innerWriter, 13);
                                innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                                var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                                EncodeLength(innerWriter, rsaEncryptionOid.Length);
                                innerWriter.Write(rsaEncryptionOid);
                                innerWriter.Write((byte)0x05); // NULL
                                EncodeLength(innerWriter, 0);
                                innerWriter.Write((byte)0x03); // BIT STRING
                                using (var bitStringStream = new MemoryStream())
                                {
                                    var bitStringWriter = new BinaryWriter(bitStringStream);
                                    bitStringWriter.Write((byte)0x00); // # of unused bits
                                    bitStringWriter.Write((byte)0x30); // SEQUENCE
                                    using (var paramsStream = new MemoryStream())
                                    {
                                        var paramsWriter = new BinaryWriter(paramsStream);
                                        EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                                        EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                                        var paramsLength = (int)paramsStream.Length;
                                        EncodeLength(bitStringWriter, paramsLength);
                                        bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                                    }
                                    var bitStringLength = (int)bitStringStream.Length;
                                    EncodeLength(innerWriter, bitStringLength);
                                    innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                                }
                                var length = (int)innerStream.Length;
                                EncodeLength(writer, length);
                                writer.Write(innerStream.GetBuffer(), 0, length);
                            }

                            var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                            output.WriteLine("-----BEGIN PUBLIC KEY-----");
                            for (var i = 0; i < base64.Length; i += 64)
                            {
                                output.WriteLine(base64, i, Math.Min(64, base64.Length - i));
                            }
                            output.WriteLine("-----END PUBLIC KEY-----");
                        }
                        return output.ToString();
                    }
                }

                private static void EncodeLength(BinaryWriter stream, int length)
                {
                    if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
                    if (length < 0x80)
                    {
                        // Short form
                        stream.Write((byte)length);
                    }
                    else
                    {
                        // Long form
                        var temp = length;
                        var bytesRequired = 0;
                        while (temp > 0)
                        {
                            temp >>= 8;
                            bytesRequired++;
                        }
                        stream.Write((byte)(bytesRequired | 0x80));
                        for (var i = bytesRequired - 1; i >= 0; i--)
                        {
                            stream.Write((byte)(length >> (8 * i) & 0xff));
                        }
                    }
                }

                private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
                {
                    stream.Write((byte)0x02); // INTEGER
                    var prefixZeros = 0;
                    for (var i = 0; i < value.Length; i++)
                    {
                        if (value[i] != 0) break;
                        prefixZeros++;
                    }
                    if (value.Length - prefixZeros == 0)
                    {
                        EncodeLength(stream, 1);
                        stream.Write((byte)0);
                    }
                    else
                    {
                        if (forceUnsigned && value[prefixZeros] > 0x7f)
                        {
                            // Add a prefix zero to force unsigned if the MSB is 1
                            EncodeLength(stream, value.Length - prefixZeros + 1);
                            stream.Write((byte)0);
                        }
                        else
                        {
                            EncodeLength(stream, value.Length - prefixZeros);
                        }
                        for (var i = prefixZeros; i < value.Length; i++)
                        {
                            stream.Write(value[i]);
                        }
                    }

                }
            }

            #endregion
        }
    }
}
