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
using System.Threading.Tasks;
using Compendio.Utils;

#endregion
namespace Compendio.Core.Data.IO
{
    /// <summary>
    /// Native and managed tools for reading, writing, and wiping files within the Windows filesystem.
    /// </summary>
    public class FileManager
    {
        #region Variables

        private bool isInitialized = false;
        private string _workingPath;
        private byte[] _fileContents;
        private bool _overwriteFile;
        /// <summary>
        /// A string representation of the current path data should be written to.
        /// </summary>
        public string WorkingPath
        {
            get { return _workingPath; }
            set { _workingPath = value; }
        }
        /// <summary>
        /// Data which will be written to the current working path.
        /// </summary>
        public byte[] FileContents
        {
            get { return _fileContents; }
            set { _fileContents = value; }
        }
        /// <summary>
        /// Determines whether or not to overwrite the working path if the file already exists.
        /// </summary>
        public bool OverwriteFile
        {
            get { return _overwriteFile; }
            set { _overwriteFile = value; }
        }
        /// <summary>
        /// Occurs when a chunk of data has been written to the current working path.
        /// </summary>
        public event FileContentsWrittenEventHandler OnFileContentsWritten;
        /// <summary>
        /// Occurs when all data has been written to the current working path.
        /// </summary>
        public event FileWriteCompleteEventHandler OnFileWriteComplete;
        /// <summary>
        /// Occurs when a chunk of data has been read from the current working path.
        /// </summary>
        public event FileContentsReadEventHandler OnFileContentsRead;
        /// <summary>
        /// Occurs when all data has been read from the current working path.
        /// </summary>
        public event FileReadCompleteEventHandler OnFileReadComplete;
        /// <summary>
        /// Occurs when a file has been successfully truncated.
        /// </summary>
        public event FileTruncatedEventHandler OnFileTruncated;
        private static FileManager Instance { get; set; } = null;

        #endregion
        #region Initialization

        /// <summary>
        /// Deafult constructor which allows access to non-static management tools.
        /// </summary>
        public FileManager() { }
        /// <summary>
        /// Initializes the file management object with data and a path to write to without explicitly setting the object's properties.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        public FileManager(string path, string contents, bool overwrite)
        {
            WorkingPath = path;
            FileContents = contents.ToBytes();
            OverwriteFile = overwrite;
            isInitialized = true;
        }
        /// <summary>
        /// Initializes the file management object with data and a path to write to without explicitly setting the object's properties.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        public FileManager(string path, byte[] contents, bool overwrite)
        {
            WorkingPath = path;
            FileContents = contents;
            OverwriteFile = overwrite;
            isInitialized = true;
        }

        #endregion
        #region Methods
        
        /// <summary>
        /// Provides access to the underlying <see cref="FileManager"/> object and its properties.
        /// </summary>
        /// <returns>The underlying <see cref="FileManager"/> object.</returns>
        public FileManager GetInstance()
        {
            return this;
        }
        /// <summary>
        /// Writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public bool WriteFile(string path = null, string contents = null, bool overwrite = false)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents.ToBytes();
            bool overwriteFile = (isInitialized) ? _overwriteFile : overwrite;
            return Write(filePath, fileContents, overwriteFile);
        }
        /// <summary>
        /// Writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public bool WriteFile(string path = null, byte[] contents = null, bool overwrite = false)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents;
            bool overwriteFile = (isInitialized) ? _overwriteFile : overwrite;
            return Write(filePath, fileContents, overwriteFile);
        }
        /// <summary>
        /// Appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public bool AppendFile(string path = null, string contents = null)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents.ToBytes();
            return Append(filePath, fileContents);
        }
        /// <summary>
        /// Appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public bool AppendFile(string path = null, byte[] contents = null)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents;
            return Append(filePath, fileContents);
        }
        /// <summary>
        /// Reads data from a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be read.</param>
        /// <returns>Data read from a file located at the provided path.</returns>
        public byte[] ReadFile(string path)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            return Read(filePath);
        }
        /// <summary>
        /// Truncates a file by explicitly setting it's size to zero.
        /// </summary>
        /// <param name="path">The location of the file to be truncated.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public bool TruncateFile(string path)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            return Truncate(filePath);
        }
        /// <summary>
        /// Asynchronously writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public async Task<FileManagerTaskResult> WriteFileAsync(string path = null, string contents = null, bool overwrite = false)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents.ToBytes();
            bool overwriteFile = (isInitialized) ? _overwriteFile : overwrite;
            return await WriteAsync(filePath, fileContents, overwriteFile);
        }
        /// <summary>
        /// Asynchronously writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public async Task<FileManagerTaskResult> WriteFileAsync(string path = null, byte[] contents = null, bool overwrite = false)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents;
            bool overwriteFile = (isInitialized) ? _overwriteFile : overwrite;
            return await WriteAsync(filePath, fileContents, overwriteFile);
        }
        /// <summary>
        /// Asynchronously appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public async Task<FileManagerTaskResult> AppendFileAsync(string path = null, string contents = null)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents.ToBytes();
            return await AppendAsync(filePath, fileContents);
        }
        /// <summary>
        /// Asynchronously appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public async Task<FileManagerTaskResult> AppendFileAsync(string path = null, byte[] contents = null)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            byte[] fileContents = (contents == null) ? _fileContents : contents;
            return await AppendAsync(WorkingPath, FileContents);
        }
        /// <summary>
        /// Asynchronously reads data from a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be read.</param>
        /// <returns>Data read from a file located at the provided path.</returns>
        public async Task<FileManagerTaskResult> ReadFileAsync(string path = null)
        {
            Instance = this;
            string filePath = (path == null) ? _workingPath : path;
            return await ReadAsync(filePath);
        }
        /// <summary>
        /// Writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static bool Write(string path, string contents, bool overwrite = false)
        {
            return Write(path, contents.ToBytes(), overwrite);
        }
        /// <summary>
        /// Writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static bool Write(string path, byte[] contents, bool overwrite = false)
        {
            // Check if the file's path exists.
            string filepath = Path.GetDirectoryName(path);
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            // Write our file's bytes to disk.
            FileMode mode = (overwrite) ? FileMode.Create : FileMode.CreateNew;
            using (FileStream fs = new FileStream(path, mode))
            {
                try
                {
                    fs.Write(contents, 0, contents.Length);
                    fs.Flush();
                    fs.Close();
                    Instance.FileWriteComplete(path);
                }
                catch (Exception ex)
                {
                    fs.Flush();
                    fs.Close();
                    throw ex;
                }
            }
            return true;
        }
        /// <summary>
        /// Appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static bool Append(string path, string contents)
        {
            return Append(path, contents.ToBytes());
        }
        /// <summary>
        /// Appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static bool Append(string path, byte[] contents)
        {
            // Write to the end of an existing file.
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    try
                    {
                        fs.Write(contents, 0, contents.Length);
                        fs.Flush();
                        fs.Close();
                        Instance.FileWriteComplete(path);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        fs.Flush();
                        fs.Close();
                        throw ex;
                    }
                }
            }
            else
                throw new FileNotFoundException("The provided file could not be found.");
        }
        /// <summary>
        /// Reads data from a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be read.</param>
        /// <returns>Data read from a file located at the provided path.</returns>
        public static byte[] Read(string path)
        {
            // Read all contents of a file.
            if (File.Exists(path))
            {
                byte[] buffer = new byte[new FileInfo(path).Length];
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    try
                    {
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Flush();
                        fs.Close();
                        Instance.FileContentsRead(path, buffer);
                        Instance.FileReadComplete(path);
                        return buffer;
                    }
                    catch (Exception ex)
                    {
                        fs.Flush();
                        fs.Close();
                        throw ex;
                    }
                }
            }
            else
                throw new FileNotFoundException("The provided file could not be found.");
        }
        /// <summary>
        /// Truncates a file by explicitly setting it's size to zero.
        /// </summary>
        /// <param name="path">The location of the file to be truncated.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static bool Truncate(string path)
        {
            // Truncate a file to zero bytes.
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    try
                    {
                        fs.SetLength(0);
                        fs.Flush();
                        fs.Close();
                        Instance.FileTruncated(path);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        fs.Flush();
                        fs.Close();
                        throw ex;
                    }
                }
            }
            else
                throw new FileNotFoundException("The provided file could not be found.");
        }
        /// <summary>
        /// Asynchronously writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static async Task<FileManagerTaskResult> WriteAsync(string path, string contents, bool overwrite)
        {
            return await WriteAsync(path, contents.ToBytes(), overwrite);
        }
        /// <summary>
        /// Asynchronously writes data to a given path and explicitly overwrites the file if it exists.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <param name="overwrite">A flag to determine whether or not to overwrite the file located at the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static async Task<FileManagerTaskResult> WriteAsync(string path, byte[] contents, bool overwrite)
        {
            // Check if the file's path exists.
            string filepath = Path.GetDirectoryName(path);
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            // Write our file's bytes to disk.
            FileMode mode = (overwrite) ? FileMode.Create : FileMode.CreateNew;
            using (FileStream fs = new FileStream(path, mode))
            {
                try
                {
                    await fs.WriteAsync(contents, 0, contents.Length);
                    fs.Flush();
                    fs.Close();
                    Instance.FileWriteComplete(path);
                }
                catch (Exception ex)
                {
                    fs.Flush();
                    fs.Close();
                    return new FileManagerTaskResult(false, null, ex);
                }
            }
            return new FileManagerTaskResult(true);
        }
        /// <summary>
        /// Asynchronously appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static async Task<FileManagerTaskResult> AppendAsync(string path, string contents)
        {
            return await AppendAsync(path, contents.ToBytes());
        }
        /// <summary>
        /// Asynchronously appends data to the end of a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be written to.</param>
        /// <param name="contents">Data which will be written to the current working path.</param>
        /// <returns>A flag which determines whether or not the operation was successful.</returns>
        public static async Task<FileManagerTaskResult> AppendAsync(string path, byte[] contents)
        {
            // Write to the end of an existing file.
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    try
                    {
                        await fs.WriteAsync(contents, 0, contents.Length);
                        fs.Flush();
                        fs.Close();
                        Instance.FileWriteComplete(path);
                        return new FileManagerTaskResult(true);
                    }
                    catch (Exception ex)
                    {
                        fs.Flush();
                        fs.Close();
                        return new FileManagerTaskResult(false, null, ex);
                    }
                }
            }
            else
                return new FileManagerTaskResult(false, null, new FileNotFoundException("The provided file could not be found."));
        }
        /// <summary>
        /// Asynchronously reads data from a file at a given path.
        /// </summary>
        /// <param name="path">The location of the file to which data will be read.</param>
        /// <returns>Data read from a file located at the provided path.</returns>
        public static async Task<FileManagerTaskResult> ReadAsync(string path)
        {
            // Read all contents of a file.
            if (File.Exists(path))
            {
                byte[] buffer = new byte[new FileInfo(path).Length];
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    try
                    {
                        await fs.ReadAsync(buffer, 0, buffer.Length);
                        fs.Flush();
                        fs.Close();
                        Instance.FileContentsRead(path, buffer);
                        Instance.FileReadComplete(path);
                        return new FileManagerTaskResult(true);
                    }
                    catch (Exception ex)
                    {
                        fs.Flush();
                        fs.Close();
                        return new FileManagerTaskResult(false, null, ex);
                    }
                }
            }
            else
                return new FileManagerTaskResult(false, null, new FileNotFoundException("The provided file could not be found."));
        }
        private void FileContentsWritten(string file, byte[] contents, long remaining)
        {
            if (Instance.OnFileContentsWritten == null) return;
            OnFileContentsWritten(this, new FileContentsWrittenEventArgs(file, contents, remaining));
        }
        private void FileWriteComplete(string file)
        {
            if (Instance.OnFileWriteComplete == null) return;
            OnFileWriteComplete(this, new FileWriteCompleteEventArgs(file));
        }
        private void FileContentsRead(string file, byte[] read)
        {
            if (Instance.OnFileContentsRead == null) return;
            OnFileContentsRead(this, new FileContentsReadEventArgs(file, read));
        }
        private void FileReadComplete(string file)
        {
            if (Instance.OnFileReadComplete == null) return;
            OnFileReadComplete(this, new FileReadCompleteEventArgs(file));
        }
        private void FileTruncated(string file)
        {
            if (Instance.OnFileTruncated == null) return;
            OnFileTruncated(this, new FileTruncatedEventArgs(file));
        }

        #endregion
    }
    #region Structs

    /// <summary>
    /// Encapsulates any returned data, errors, and a flag to determine if the task was succesful or not.
    /// </summary>
    public struct FileManagerTaskResult
    {
        public bool Successful { get; private set; }
        public byte[] Data { get; private set; }
        public Exception Error { get; private set; }
        public FileManagerTaskResult(bool successful, byte[] data = null, Exception error = null)
        {
            Successful = successful;
            Data = data;
            Error = error;
        }
    }

    #endregion
    #region Events

    /// <summary>
    /// Represents the method that will handle the <see cref="FileManager.OnFileContentsWritten"/> event of a <see cref="FileManager"/> object. 
    /// </summary>
    public delegate void FileContentsWrittenEventHandler(object sender, FileContentsWrittenEventArgs args);
    /// <summary>
    /// Contains data for the <see cref="FileManager.OnFileContentsWritten"/> event.
    /// </summary>
    public class FileContentsWrittenEventArgs : EventArgs
    {
        public string Filename { get; private set; }
        public string Filepath { get; private set; }
        public byte[] BytesWritten { get; private set; }
        public long BytesRemaining { get; private set; }
        public FileContentsWrittenEventArgs(string file, byte[] bytesWritten, long bytesRemaining)
        {
            Filename = Path.GetFileName(file);
            Filepath = Path.GetDirectoryName(file);
            BytesWritten = bytesWritten;
            BytesRemaining = bytesRemaining;
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="FileManager.OnFileWriteComplete"/> event of a <see cref="FileManager"/> object. 
    /// </summary>
    public delegate void FileWriteCompleteEventHandler(object sender, FileWriteCompleteEventArgs args);
    /// <summary>
    /// Contains data for the <see cref="FileManager.OnFileWriteComplete"/> event.
    /// </summary>
    public class FileWriteCompleteEventArgs : EventArgs
    {
        public string Filename { get; private set; }
        public string Filepath { get; private set; }
        public FileWriteCompleteEventArgs(string file)
        {
            Filename = Path.GetFileName(file);
            Filepath = Path.GetDirectoryName(file);
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="FileManager.OnFileContentsRead"/> event of a <see cref="FileManager"/> object.
    /// </summary>
    public delegate void FileContentsReadEventHandler(object sender, FileContentsReadEventArgs args);
    /// <summary>
    /// Contains data for the <see cref="FileManager.OnFileContentsRead"/> event.
    /// </summary>
    public class FileContentsReadEventArgs : EventArgs
    {
        public string Filename { get; private set; }
        public string Filepath { get; private set; }
        public byte[] BytesRead { get; private set; }
        public FileContentsReadEventArgs(string file, byte[] bytesRead)
        {
            Filename = Path.GetFileName(file);
            Filepath = Path.GetDirectoryName(file);
            BytesRead = bytesRead;
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="FileManager.OnFileReadComplete"/> event of a <see cref="FileManager"/> object.
    /// </summary>
    public delegate void FileReadCompleteEventHandler(object sender, FileReadCompleteEventArgs args);
    /// <summary>
    /// Contains data for the <see cref="FileManager.OnFileReadComplete"/> event.
    /// </summary>
    public class FileReadCompleteEventArgs : EventArgs
    {
        public string Filename { get; private set; }
        public string Filepath { get; private set; }
        public FileReadCompleteEventArgs(string file)
        {
            Filename = Path.GetFileName(file);
            Filepath = Path.GetDirectoryName(file);
        }
    }

    /// <summary>
    /// Represents the method that will handle the <see cref="FileManager.OnFileTruncated"/> event of a <see cref="FileManager"/> object.
    /// </summary>
    public delegate void FileTruncatedEventHandler(object sender, FileTruncatedEventArgs args);
    /// <summary>
    /// Contains data for the <see cref="FileManager.OnFileTruncated"/> event.
    /// </summary>
    public class FileTruncatedEventArgs : EventArgs
    {
        public string Filename { get; private set; }
        public string Filepath { get; private set; }
        public FileTruncatedEventArgs(string file)
        {
            Filename = Path.GetFileName(file);
            Filepath = Path.GetDirectoryName(file);
        }
    }

    #endregion
}