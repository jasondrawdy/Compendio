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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LongFile = Pri.LongPath.File;

#endregion
namespace Compendio.Core.Data.Security
{
    // TODO: Implement events in order to track progress more accurately.

    /// <summary>
    /// Allows the creation of a file of a particular size in kilobytes, megabytes, or gigabytes.
    /// </summary>
    public class FileCreator
    {
        #region Variables
        
        /// <summary>
        /// Common size types that a created file will be considering modern storage mediums.
        /// </summary>
        public enum FileSizeType { Kilobytes, Megabytes, Gigabytes }

        /// <summary>
        /// A flag which will determine whether or not any currently running threads should stop gracefully.
        /// </summary>
        public bool IsCancelling { get; private set; } = false;
        private static CancellationToken CancelToken = new CancellationToken();
        //public event GenerateFileProgressChangedEventHandler OnProgressChanged;

        #endregion
        #region Initialization

        /// <summary>
        /// Allows the creation of a file of a particular size in kilobytes, megabytes, or gigabytes.
        /// </summary>
        public FileCreator() { }

        #endregion
        #region Methods

        /// <summary>
        /// Creates a file of a specified size at a given location containing cryptographically randomized data.
        /// </summary>
        /// <param name="path">The location which the file should be created.</param>
        /// <param name="size">The size which the created size will be relative to the chosen <see cref="FileSizeType"/>.</param>
        /// <param name="type">The size type which determines how large the created file will be.</param>
        /// <returns>A flag determining whether the operation was successful.</returns>
        public bool GenerateFile(string path, int size, FileSizeType type)
        {
            try
            {
                if (!File.Exists(path))
                {
                    // Create our default buffer and block sizes.
                    int bufferSize = 1024 * 1024;
                    int blockSize = (1024) / bufferSize;

                    // Check our size type and calculate the actual buffer and block length.
                    switch (type)
                    {
                        case FileSizeType.Kilobytes:
                            bufferSize = 1024;
                            blockSize = (1024) / bufferSize;
                            break;
                        case FileSizeType.Megabytes:
                            blockSize = (1024 * 1024) / bufferSize;
                            break;
                        case FileSizeType.Gigabytes:
                            blockSize = (1024 * 1024 * 1024) / bufferSize;
                            break;
                        default:
                            return false;
                    }

                    // Generate random data and write it to disk.
                    using (var file = LongFile.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] data = new byte[bufferSize];
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        for (int i = 0; i < size * blockSize; i++)
                        {
                            rng.GetBytes(data);
                            file.Write(data, 0, data.Length);
                        }
                        file.Flush();
                        file.Close();
                    }
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Creates a file of a specified size at a given location containing cryptographically randomized data.
        /// </summary>
        /// <param name="path">The location which the file should be created.</param>
        /// <param name="size">The size which the created size will be relative to the chosen <see cref="FileSizeType"/>.</param>
        /// <param name="type">The size type which determines how large the created file will be.</param>
        /// <returns>A flag determining whether the operation was successful.</returns>
        public async Task<bool> GenerateFileAsync(string path, int size, FileSizeType type)
        {
            try
            {
                if (!File.Exists(path))
                {
                    // Create our default buffer and block sizes.
                    //float totalBytes = 0;
                    //float bytesWritten = 0;
                    int bufferSize = 1024 * 1024;
                    int blockSize = (1024) / bufferSize;

                    // Check our size type and calculate the actual buffer and block length.
                    switch (type)
                    {
                        case FileSizeType.Kilobytes:
                            bufferSize = 1024;
                            blockSize = (1024) / bufferSize;
                            break;
                        case FileSizeType.Megabytes:
                            blockSize = (1024 * 1024) / bufferSize;
                            break;
                        case FileSizeType.Gigabytes:
                            blockSize = (1024 * 1024 * 1024) / bufferSize;
                            break;
                        default:
                            return false;
                    }

                    // Generate random data and write it to disk.
                    using (var file = LongFile.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] data = new byte[bufferSize];
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        //totalBytes = (size * blockSize) * bufferSize;
                        for (int i = 0; i < size * blockSize; i++)
                        {
                            rng.GetBytes(data);
                            await file.WriteAsync(data, 0, data.Length, CancelToken);
                            //bytesWritten += data.Length;
                            //float progress = ((bytesWritten / totalBytes) * 100);
                            //OnGenerateFileProgressChanged(new GenerateFileProgressChangedEventArgs((int)(progress)));
                        }
                        file.Flush();
                        file.Close();
                    }

                    return true;
                }
                else { return false; }
            }
            catch { return false; }
        }

        /// <summary>
        /// Stops all currently running threads and allows them to shut down gracefully.
        /// </summary>
        public void Stop()
        {
            IsCancelling = true;
            CancelToken = new CancellationToken(true);
        }

        #endregion
    }
    #region Events

    //#region Delegates

    //public delegate void GenerateFileProgressChangedEventHandler(object sender, GenerateFileProgressChangedEventArgs args);

    //#endregion
    //public class GenerateFileProgressChangedEventArgs : EventArgs
    //{
    //    #region Variables

    //    public int Progress { get; private set; }

    //    #endregion
    //    #region Initialization

    //    public GenerateFileProgressChangedEventArgs(int progress)
    //    {
    //        Progress = progress;
    //    }

    //    #endregion
    //}

    #endregion
}
