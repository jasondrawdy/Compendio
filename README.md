<p align="center">
    <img width="256" height="256" src="https://user-images.githubusercontent.com/40871836/43354821-6fa4e332-9218-11e8-94b9-75ccb368756f.png">
</p>

# Compendio
<p align="left">
    <!-- Version -->
    <img src="https://img.shields.io/badge/version-1.0.0-brightgreen.svg">
    <!-- Docs -->
    <img src="https://img.shields.io/badge/docs-not%20found-lightgrey.svg">
    <!-- License -->
    <img src="https://img.shields.io/badge/license-MIT-blue.svg">
</p>

Compendio is a collection of common and noteworthy extension methods, security tools, and filesystem functions generally found in most applications; focusing on extensibility and portability while also keeping code and documentation easy to follow and clear to understand. Most tools found in the library are usually written everytime a developer/team creates a new project which then becomes a repetitive task, and so, this is where Compendio comes in. Being a portable library a developer can reference the library, or copy the source code directly into their project, providing them with more time to focus on business logic and or the core code of their main application.

---

**Note:** *Compendio uses libraries that contain "unsafe" code and therefore the `Allow unsafe code` build option must be selected in order to actually build the project. However, this shouldn't be a problem considering that the option has already enabled in the project solution prior to pushing to Github.*

---

### Requirements
- .NET Framework 4.6.1

# Features
Compendio comes with a notable amount of tools which range from common extensions which transform strings all the way to methods used to manipulate the local filesystem either in a managed or native way. There are extensions that exist for nearly every common C# type as well as tools for security and constants for certain fields of study such as Mathematics, Physics, and Chemistry. The currently available features are the following:

### Common Tools
- **Constants**
    - Mathematics
    - Physics
    - Chemistry

### Data Utilities
- **IO**
    - Directories
        - *Manage files and folders using managed and native APIs*
        - *Explore long paths otherwise restricted by the 260 character path length limit*
    - Files
        - *Static and constructed reading, writing, and appending of files using a `FileManager` object*
        - *A large list of common `MimeTypes` in a single dictionary*
        - *Create files with a path name longer than the typically restrictive 260 character path length limit*
- **Security**
    - `SecureRandom` — a cryptographically strong random object utilizing a `RNGCryptoServiceProvider`
    - Generators
        - *Create files of a specified size containing cryptographically strong data*
        - *Generate crypto strong passwords of any length, type, and with or without non-alphanumerics*
    - Cryptography
        - *Encrypt and decrypt files both synchronously and asynchronously*
        - *Encrypt and decrypt strings both synchronously and asynchronously*
        - *Encrypt and decrypt data both synchronously and asynchronously*
    - Hashing
        - *Generate checksums of files, strings, and byte data*
            - ***MD5***
            - ***RIPEMD150***
            - ***SHA1***
            - ***SHA256***
            - ***SHA384***
            - ***SHA512***

### Extensions
- **Collections**
    - Search, sort, and merge collections of varying types
- **Transformations**
    - Int
        - *Convert most common types to an `int`*
    - Double
        - *Round a double precision number using the provided precision and  `MidpointRounding` object*
    - Byte
        - *Convert an `int` or `string` to a byte array*
        - *Convert a Base64 encoded `string` into a byte array filled with its decoded equivalent*
        - *Convert a `byte[]` into a byte array filled with its Base64 encoded equivalent*
        - *Encrypt an array of bytes*
        - *Decrypt an array of bytes*
        - *Merge two byte arrays into a single array*
    - String
        - *Obtain the UTF8 encoded `string` equivalent of a byte array*
        - *Obtain the Base64 encoded `string` equivalent of a byte array*
        - *Convert a `long` into a file size represented as a `string`*
        - *Split a `string` into an `IEnumerable`*
        - *Create a `List<string>` from a `string`*
        - *Reverse a `string`*
        - *Encrypt a `string` using AES256 in CBC mode*
        - *Decrypt a `string` using AES256 in CBC mode*
        - *Convert a `string` into its Base64 equivalent*
        - *Convert a Base64 encoded `string` into its plaintext equivalent*
    - Color
        - *Convert an RGB color from a string into its `Color` object equivalent*
    - XElement
        - *Extract an `XElement` from an XML file given the element name*
        - *Obtain a value from a specified `XElement` without throwing an exception*
    - Data
        - *Serialize and deserialize any object into an array of bytes that has the `[Serializable]` property*
    - Formatters
        - *Contains an interface to convert a file size of `long` into its shorthand `string` representation*
- **Validation**
    - Check if a `string` is a valid email address in context of modern email addresses

# Credits
**Icon:** `Prosymbols` <br>
https://www.flaticon.com/authors/prosymbols <br>

**Encryption:** `sdrapkin` <br>
https://github.com/sdrapkin/SecurityDriven.Inferno <br>

**Long Paths:** `peteraritchie` <br>
https://github.com/peteraritchie/LongPath <br>

# License
Copyright © ∞ Jason Drawdy 

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
