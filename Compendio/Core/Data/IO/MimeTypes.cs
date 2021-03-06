﻿/*
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
using System.Collections.Generic;

#endregion
namespace Compendio.Core.Data.IO
{
    public static class MimeTypeMap
    {
        #region Variables

        private static readonly IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region Mimetypes

        /* Combination of values from Windows 7 Registry and 
           from C:\Windows\System32\inetsrv\config\applicationHost.config
           some added, including .7z and .dat */
        {".323", "Text"},
        {".3g2", "Video"},
        {".3gp", "Video"},
        {".3gp2", "Video"},
        {".3gpp", "Video"},
        {".7z", "App"},
        {".aa", "Audio"},
        {".AAC", "Audio"},
        {".aaf", "App"},
        {".aax", "Audio"},
        {".ac3", "Audio"},
        {".aca", "App"},
        {".accda", "App"},
        {".accdb", "App"},
        {".accdc", "App"},
        {".accde", "App"},
        {".accdr", "App"},
        {".accdt", "App"},
        {".accdw", "App"},
        {".accft", "App"},
        {".acx", "App"},
        {".AddIn", "Text"},
        {".ade", "App"},
        {".adobebridge", "App"},
        {".adp", "App"},
        {".ADT", "Audio"},
        {".ADTS", "Audio"},
        {".afm", "App"},
        {".ai", "App"},
        {".aif", "Audio"},
        {".aifc", "Audio"},
        {".aiff", "Audio"},
        {".air", "App"},
        {".amc", "App"},
        {".application", "App"},
        {".art", "Image"},
        {".asa", "App"},
        {".asax", "App"},
        {".ascx", "App"},
        {".asd", "App"},
        {".asf", "Video"},
        {".ashx", "App"},
        {".asi", "App"},
        {".asm", "Text"},
        {".asmx", "App"},
        {".aspx", "App"},
        {".asr", "Video"},
        {".asx", "Video"},
        {".atom", "App"},
        {".au", "Audio"},
        {".avi", "Video"},
        {".axs", "App"},
        {".bas", "Text"},
        {".bcpio", "App"},
        {".bin", "App"},
        {".bmp", "Image"},
        {".c", "Text"},
        {".cab", "App"},
        {".caf", "Audio"},
        {".calx", "App"},
        {".cat", "App"},
        {".cc", "Text"},
        {".cd", "Text"},
        {".cdda", "Audio"},
        {".cdf", "App"},
        {".cer", "App"},
        {".chm", "App"},
        {".class", "App"},
        {".clp", "App"},
        {".cmx", "Image"},
        {".cnf", "Text"},
        {".cod", "Image"},
        {".config", "App"},
        {".contact", "Text"},
        {".coverage", "App"},
        {".cpio", "App"},
        {".cpp", "Code"},
        {".crd", "App"},
        {".crl", "App"},
        {".crt", "App"},
        {".cs", "Code"},
        {".csdproj", "Text"},
        {".csh", "App"},
        {".csproj", "Code"},
        {".css", "Text"},
        {".csv", "Text"},
        {".cur", "App"},
        {".cxx", "Text"},
        {".dat", "App"},
        {".datasource", "App"},
        {".dbproj", "Text"},
        {".dcr", "App"},
        {".def", "Text"},
        {".deploy", "App"},
        {".der", "App"},
        {".dgml", "App"},
        {".dib", "Image"},
        {".dif", "Video"},
        {".dir", "App"},
        {".disco", "Text"},
        {".divx", "Video"},
        {".dll", "App"},
        {".dll.config", "Text"},
        {".dlm", "Text"},
        {".doc", "App"},
        {".docm", "App"},
        {".docx", "App"},
        {".dot", "App"},
        {".dotm", "App"},
        {".dotx", "App"},
        {".dsp", "App"},
        {".dsw", "Text"},
        {".dtd", "Text"},
        {".dtsConfig", "Text"},
        {".dv", "Video"},
        {".dvi", "App"},
        {".dwf", "Drawing"},
        {".dwp", "App"},
        {".dxr", "App"},
        {".eml", "Message"},
        {".emz", "App"},
        {".eot", "App"},
        {".eps", "App"},
        {".etl", "App"},
        {".etx", "Text"},
        {".evy", "App"},
        {".exe", "App"},
        {".exe.config", "Text"},
        {".fdf", "App"},
        {".fif", "App"},
        {".filters", "App"},
        {".fla", "App"},
        {".flr", "X-World"},
        {".flv", "Video"},
        {".fsscript", "App"},
        {".fsx", "App"},
        {".generictest", "App"},
        {".gif", "Image"},
        {".group", "Text"},
        {".gsm", "Audio"},
        {".gtar", "App"},
        {".gz", "App"},
        {".h", "Code"},
        {".hdf", "App"},
        {".hdml", "Text"},
        {".hhc", "App"},
        {".hhk", "App"},
        {".hhp", "App"},
        {".hlp", "App"},
        {".hpp", "Text"},
        {".hqx", "App"},
        {".hta", "App"},
        {".htc", "Text"},
        {".htm", "Code"},
        {".html", "Code"},
        {".htt", "Text"},
        {".hxa", "App"},
        {".hxc", "App"},
        {".hxd", "App"},
        {".hxe", "App"},
        {".hxf", "App"},
        {".hxh", "App"},
        {".hxi", "App"},
        {".hxk", "App"},
        {".hxq", "App"},
        {".hxr", "App"},
        {".hxs", "App"},
        {".hxt", "Text"},
        {".hxv", "App"},
        {".hxw", "App"},
        {".hxx", "Text"},
        {".i", "Text"},
        {".ico", "Image"},
        {".ics", "App"},
        {".idl", "Text"},
        {".ief", "Image"},
        {".iii", "App"},
        {".inc", "Text"},
        {".inf", "App"},
        {".inl", "Text"},
        {".ins", "App"},
        {".ipa", "App"},
        {".ipg", "App"},
        {".ipproj", "Text"},
        {".ipsw", "App"},
        {".iqy", "Text"},
        {".isp", "App"},
        {".ite", "App"},
        {".itlp", "App"},
        {".itms", "App"},
        {".itpc", "App"},
        {".IVF", "Video"},
        {".jar", "App"},
        {".java", "App"},
        {".jck", "App"},
        {".jcz", "App"},
        {".jfif", "Image"},
        {".jnlp", "App"},
        {".jpb", "App"},
        {".jpe", "Image"},
        {".jpeg", "Image"},
        {".jpg", "Image"},
        {".js", "App"},
        {".json", "App"},
        {".jsx", "Text"},
        {".jsxbin", "Text"},
        {".latex", "App"},
        {".library-ms", "App"},
        {".lit", "App"},
        {".loadtest", "App"},
        {".lpk", "App"},
        {".lsf", "Video"},
        {".lst", "Text"},
        {".lsx", "Video"},
        {".lzh", "App"},
        {".m13", "App"},
        {".m14", "App"},
        {".m1v", "Video"},
        {".m2t", "Video"},
        {".m2ts", "Video"},
        {".m2v", "Video"},
        {".m3u", "Audio"},
        {".m3u8", "Audio"},
        {".m4a", "Audio"},
        {".m4b", "Audio"},
        {".m4p", "Audio"},
        {".m4r", "Audio"},
        {".m4v", "Video"},
        {".mac", "Image"},
        {".mak", "Text"},
        {".man", "App"},
        {".manifest", "App"},
        {".map", "Text"},
        {".master", "App"},
        {".mda", "App"},
        {".mdb", "App"},
        {".mde", "App"},
        {".mdp", "App"},
        {".me", "App"},
        {".mfp", "App"},
        {".mht", "Message"},
        {".mhtml", "Message"},
        {".mid", "Audio"},
        {".midi", "Audio"},
        {".mix", "App"},
        {".mk", "Text"},
        {".mmf", "App"},
        {".mno", "Text"},
        {".mny", "App"},
        {".mod", "Video"},
        {".mov", "Video"},
        {".movie", "Video"},
        {".mp2", "Video"},
        {".mp2v", "Video"},
        {".mp3", "Audio"},
        {".mp4", "Video"},
        {".mp4v", "Video"},
        {".mpa", "Video"},
        {".mpe", "Video"},
        {".mpeg", "Video"},
        {".mpf", "App"},
        {".mpg", "Video"},
        {".mpp", "App"},
        {".mpv2", "Video"},
        {".mqv", "Video"},
        {".ms", "App"},
        {".msi", "App"},
        {".mso", "App"},
        {".mts", "Video"},
        {".mtx", "App"},
        {".mvb", "App"},
        {".mvc", "App"},
        {".mxp", "App"},
        {".nc", "App"},
        {".nsc", "Video"},
        {".nws", "Message"},
        {".ocx", "App"},
        {".oda", "App"},
        {".odb", "App"},
        {".odc", "App"},
        {".odf", "App"},
        {".odg", "App"},
        {".odh", "Text"},
        {".odi", "App"},
        {".odl", "Text"},
        {".odm", "App"},
        {".odp", "App"},
        {".ods", "App"},
        {".odt", "App"},
        {".ogv", "Video"},
        {".one", "App"},
        {".onea", "App"},
        {".onepkg", "App"},
        {".onetmp", "App"},
        {".onetoc", "App"},
        {".onetoc2", "App"},
        {".orderedtest", "App"},
        {".osdx", "App"},
        {".otg", "App"},
        {".oth", "App"},
        {".otp", "App"},
        {".ots", "App"},
        {".ott", "App"},
        {".oxt", "App"},
        {".p10", "App"},
        {".p12", "App"},
        {".p7b", "App"},
        {".p7c", "App"},
        {".p7m", "App"},
        {".p7r", "App"},
        {".p7s", "App"},
        {".pbm", "Image"},
        {".pcast", "App"},
        {".pct", "Image"},
        {".pcx", "App"},
        {".pcz", "App"},
        {".pdf", "App"},
        {".pfb", "App"},
        {".pfm", "App"},
        {".pfx", "App"},
        {".pgm", "Image"},
        {".pic", "Image"},
        {".pict", "Image"},
        {".pkgdef", "Text"},
        {".pkgundef", "Text"},
        {".pko", "App"},
        {".pls", "Audio"},
        {".pma", "App"},
        {".pmc", "App"},
        {".pml", "App"},
        {".pmr", "App"},
        {".pmw", "App"},
        {".png", "Image"},
        {".pnm", "Image"},
        {".pnt", "Image"},
        {".pntg", "Image"},
        {".pnz", "Image"},
        {".pot", "App"},
        {".potm", "App"},
        {".potx", "App"},
        {".ppa", "App"},
        {".ppam", "App"},
        {".ppm", "Image"},
        {".pps", "App"},
        {".ppsm", "App"},
        {".ppsx", "App"},
        {".ppt", "App"},
        {".pptm", "App"},
        {".pptx", "App"},
        {".prf", "App"},
        {".prm", "App"},
        {".prx", "App"},
        {".ps", "App"},
        {".psc1", "App"},
        {".psd", "App"},
        {".psess", "App"},
        {".psm", "App"},
        {".psp", "App"},
        {".pub", "App"},
        {".pwz", "App"},
        {".qht", "Text"},
        {".qhtm", "Text"},
        {".qt", "Video"},
        {".qti", "Image"},
        {".qtif", "Image"},
        {".qtl", "App"},
        {".qxd", "App"},
        {".ra", "Audio"},
        {".ram", "Audio"},
        {".rar", "App"},
        {".ras", "Image"},
        {".rat", "App"},
        {".rc", "Text"},
        {".rc2", "Text"},
        {".rct", "Text"},
        {".rdlc", "App"},
        {".resx", "App"},
        {".rf", "Image"},
        {".rgb", "Image"},
        {".rgs", "Text"},
        {".rm", "App"},
        {".rmi", "Audio"},
        {".rmp", "App"},
        {".roff", "App"},
        {".rpm", "Audio"},
        {".rqy", "Text"},
        {".rtf", "App"},
        {".rtx", "Text"},
        {".ruleset", "App"},
        {".s", "Text"},
        {".safariextz", "App"},
        {".scd", "App"},
        {".sct", "Text"},
        {".sd2", "Audio"},
        {".sdp", "App"},
        {".sea", "App"},
        {".searchConnector-ms", "App"},
        {".setpay", "App"},
        {".setreg", "App"},
        {".settings", "App"},
        {".sgimb", "App"},
        {".sgml", "Text"},
        {".sh", "Code"},
        {".shar", "App"},
        {".shtml", "Text"},
        {".sit", "App"},
        {".sitemap", "App"},
        {".skin", "App"},
        {".sldm", "App"},
        {".sldx", "App"},
        {".slk", "App"},
        {".sln", "Text"},
        {".slupkg-ms", "App"},
        {".smd", "Audio"},
        {".smi", "App"},
        {".smx", "Audio"},
        {".smz", "Audio"},
        {".snd", "Audio"},
        {".snippet", "App"},
        {".snp", "App"},
        {".sol", "Text"},
        {".sor", "Text"},
        {".spc", "App"},
        {".spl", "App"},
        {".src", "App"},
        {".srf", "Text"},
        {".SSISDeploymentManifest", "Text"},
        {".ssm", "App"},
        {".sst", "App"},
        {".stl", "App"},
        {".sv4cpio", "App"},
        {".sv4crc", "App"},
        {".svc", "App"},
        {".svg", "Image"},
        {".swf", "App"},
        {".t", "App"},
        {".tar", "App"},
        {".tcl", "App"},
        {".testrunconfig", "App"},
        {".testsettings", "App"},
        {".tex", "App"},
        {".texi", "App"},
        {".texinfo", "App"},
        {".tgz", "App"},
        {".thmx", "App"},
        {".thn", "App"},
        {".tif", "Image"},
        {".tiff", "Image"},
        {".tlh", "Text"},
        {".tli", "Text"},
        {".toc", "App"},
        {".tr", "App"},
        {".trm", "App"},
        {".trx", "App"},
        {".ts", "Video"},
        {".tsv", "Text"},
        {".ttf", "App"},
        {".tts", "Video"},
        {".txt", "Text"},
        {".u32", "App"},
        {".uls", "Text"},
        {".user", "Text"},
        {".ustar", "App"},
        {".vb", "Code"},
        {".vbdproj", "Code"},
        {".vbk", "Video"},
        {".vbproj", "Code"},
        {".vbs", "Code"},
        {".vcf", "Text"},
        {".vcproj", "Code"},
        {".vcs", "Text"},
        {".vcxproj", "App"},
        {".vddproj", "Text"},
        {".vdp", "Text"},
        {".vdproj", "Text"},
        {".vdx", "App"},
        {".vml", "Text"},
        {".vscontent", "App"},
        {".vsct", "Text"},
        {".vsd", "App"},
        {".vsi", "App"},
        {".vsix", "App"},
        {".vsixlangpack", "Text"},
        {".vsixmanifest", "Text"},
        {".vsmdi", "App"},
        {".vspscc", "Text"},
        {".vss", "App"},
        {".vsscc", "Text"},
        {".vssettings", "Text"},
        {".vssscc", "Text"},
        {".vst", "App"},
        {".vstemplate", "Text"},
        {".vsto", "App"},
        {".vsw", "App"},
        {".vsx", "App"},
        {".vtx", "App"},
        {".wav", "Audio"},
        {".wave", "Audio"},
        {".wax", "Audio"},
        {".wbk", "App"},
        {".wbmp", "Image"},
        {".wcm", "App"},
        {".wdb", "App"},
        {".wdp", "Image"},
        {".webarchive", "App"},
        {".webm", "Video"},
        {".webtest", "App"},
        {".wiq", "App"},
        {".wiz", "App"},
        {".wks", "App"},
        {".WLMP", "App"},
        {".wlpginstall", "App"},
        {".wlpginstall3", "App"},
        {".wm", "Video"},
        {".wma", "Audio"},
        {".wmd", "App"},
        {".wmf", "App"},
        {".wml", "Text"},
        {".wmlc", "App"},
        {".wmls", "Text"},
        {".wmlsc", "App"},
        {".wmp", "Video"},
        {".wmv", "Video"},
        {".wmx", "Video"},
        {".wmz", "App"},
        {".wpl", "App"},
        {".wps", "App"},
        {".wri", "App"},
        {".wrl", "X-World"},
        {".wrz", "X-World"},
        {".wsc", "Text"},
        {".wsdl", "Text"},
        {".wvx", "Video"},
        {".x", "App"},
        {".xaf", "X-World"},
        {".xaml", "App"},
        {".xap", "App"},
        {".xbap", "App"},
        {".xbm", "Image"},
        {".xdr", "Text"},
        {".xht", "App"},
        {".xhtml", "App"},
        {".xla", "App"},
        {".xlam", "App"},
        {".xlc", "App"},
        {".xld", "App"},
        {".xlk", "App"},
        {".xll", "App"},
        {".xlm", "App"},
        {".xls", "App"},
        {".xlsb", "App"},
        {".xlsm", "App"},
        {".xlsx", "App"},
        {".xlt", "App"},
        {".xltm", "App"},
        {".xltx", "App"},
        {".xlw", "App"},
        {".xml", "Text"},
        {".xmta", "App"},
        {".xof", "X-World"},
        {".XOML", "Text"},
        {".xpm", "Image"},
        {".xps", "App"},
        {".xrm-ms", "Text"},
        {".xsc", "App"},
        {".xsd", "Text"},
        {".xsf", "Text"},
        {".xsl", "Text"},
        {".xslt", "Text"},
        {".xsn", "App"},
        {".xss", "App"},
        {".xtp", "App"},
        {".xwd", "Image"},
        {".z", "App"},
        {".zip", "App"},
        {".webp", "Image"}
        #endregion
        };

        #endregion
        #region Methods

        /// <summary>
        /// Obtain the mime type of an associated file extension.
        /// </summary>
        /// <param name="extension">The extension to obtain a mime type for.</param>
        /// <returns>A mime type for an associated file extension.</returns>
        public static string GetMimeType(string extension)
        {
            if (extension == null)
                throw new ArgumentNullException("extension");

            if (!extension.StartsWith("."))
                extension = "." + extension;

            string mime;
            return _mappings.TryGetValue(extension, out mime) ? mime : "Unknown";
        }

        #endregion
    }
}
