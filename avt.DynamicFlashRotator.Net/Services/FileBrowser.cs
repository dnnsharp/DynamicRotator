using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using avt.DynamicFlashRotator.Net.Settings;

namespace avt.DynamicFlashRotator.Net.Services
{

    public class FileBrowser_Folder
    {
        string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        string _PhysicalPath;
        public string PhysicalPath { get { return _PhysicalPath; } set { _PhysicalPath = value; } }

        string _RelativePath;
        public string RelativePath { get { return _RelativePath; } set { _RelativePath = value; } }

        string _Url;
        public string Url { get { return _Url; } set { _Url = value; } }

        public bool HasSubfolders { get { return Directory.GetDirectories(PhysicalPath).Length > 0; } }


        public string ToStringJson()
        {
            return string.Format("{{\"name\":\"{0}\",\"relPath\":\"{1}\",\"hasChildren\":{2}}}", 
                RotatorSettings.JsonEncode(Name),
                RotatorSettings.JsonEncode(Url),
                HasSubfolders ? "true" : "false"
            );
        }
    }

    public class FileBrowser_File
    {
        string _Name;
        public string Name { get { return _Name; } set { _Name = value; } }

        string _PhysicalPath;
        public string PhysicalPath { get { return _PhysicalPath; } set { _PhysicalPath = value; } }

        string _RelativePath;
        public string RelativePath { get { return _RelativePath; } set { _RelativePath = value; } }

        string _Url;
        public string Url { get { return _Url; } set { _Url = value; } }

        public string ToStringJson()
        {
            return string.Format("{{\"name\":\"{0}\",\"relPath\":\"{1}\",\"fullUrl\":\"{2}\"}}",
                RotatorSettings.JsonEncode(Name),
                RotatorSettings.JsonEncode(RelativePath),
                RotatorSettings.JsonEncode(Url)
            );
        }
    }


    public class FileBrowser
    {
        string _RootName;
        public string RootName { get { return _RootName; } }

        string _RootPhysicalPath;
        public string RootPhysicalPath { get { return _RootPhysicalPath; } }

        FileBrowser_Folder _RootFolder;
        public FileBrowser_Folder RootFolder { get { return _RootFolder; } }

        Dictionary<string, bool> _ext;

        public FileBrowser(string rootPhysicalPath, string rootName, params string[] extensions)
        {
            _RootPhysicalPath = rootPhysicalPath.ToLower().Replace('/', '\\');
            _RootName = rootName;
            
            _ext = new Dictionary<string, bool>();
            foreach (string ext in extensions) {
                _ext[ext] = true;
            }

            _RootFolder = new FileBrowser_Folder();
            _RootFolder.PhysicalPath = _RootPhysicalPath;
            _RootFolder.RelativePath = _RootPhysicalPath.Replace(HttpRuntime.AppDomainAppPath.ToLower(), "").Replace('\\', '/');
            _RootFolder.Url = HttpRuntime.AppDomainAppVirtualPath.Trim('/') + "/" + _RootFolder.RelativePath.Trim('/');
            if (_RootFolder.Url == "/")
                _RootFolder.Url = "";
        }


        public List<FileBrowser_Folder> ListFolders(string relPath)
        {
            if (relPath == null)
                relPath = "";

            string parentFolder = Path.Combine(RootPhysicalPath.Trim('/').Trim('\\'), relPath.Trim('/').Trim('\\'));

            List<FileBrowser_Folder> folders = new List<FileBrowser_Folder>();
            if (!Directory.Exists(parentFolder)) {
                return folders;
            }
            
            foreach (string folderPath in Directory.GetDirectories(parentFolder)) {
                FileBrowser_Folder folder = new FileBrowser_Folder();
                folder.Name = Path.GetFileName(folderPath);
                folder.PhysicalPath = folderPath;
                folder.RelativePath = folderPath.ToLower().Replace(RootPhysicalPath, "").Replace('\\', '/');
                folder.Url = ResolveUrl(folder.RelativePath);
                folders.Add(folder);
            }

            return folders;
        }

        public List<FileBrowser_File> ListFiles(string relPath)
        {
            if (relPath == null)
                relPath = "";

            string parentFolder = Path.Combine(RootPhysicalPath.Trim('/').Trim('\\'), relPath.Trim('/').Trim('\\'));

            List<FileBrowser_File> files = new List<FileBrowser_File>();
            if (!Directory.Exists(parentFolder)) {
                return files;
            }

            foreach (string filePath in Directory.GetFiles(parentFolder)) {
                if (!_ext.ContainsKey(Path.GetExtension(filePath).Trim('.')))
                    continue;

                FileBrowser_File file = new FileBrowser_File();
                file.Name = Path.GetFileName(filePath);
                file.PhysicalPath = filePath;
                file.RelativePath = filePath.ToLower().Replace(RootPhysicalPath, "").Replace('\\', '/');
                file.Url = ResolveUrl(file.RelativePath);
                files.Add(file);
            }

            return files;
        }

        public static string ResolveUrl(string relativeUrl)
        {
            if (relativeUrl == null)
                return ""; // throw new ArgumentNullException("relativeUrl");

            if (relativeUrl.Length == 0 || relativeUrl[0] == '/' || relativeUrl[0] == '\\')
                return relativeUrl;

            int idxOfScheme = relativeUrl.IndexOf(@"://", StringComparison.Ordinal);
            if (idxOfScheme != -1) {
                int idxOfQM = relativeUrl.IndexOf('?');
                if (idxOfQM == -1 || idxOfQM > idxOfScheme) return relativeUrl;
            }

            StringBuilder sbUrl = new StringBuilder();
            sbUrl.Append(HttpRuntime.AppDomainAppVirtualPath);
            if (sbUrl.Length == 0 || sbUrl[sbUrl.Length - 1] != '/') sbUrl.Append('/');

            // found question mark already? query string, do not touch!
            bool foundQM = false;
            bool foundSlash; // the latest char was a slash?
            if (relativeUrl.Length > 1
                && relativeUrl[0] == '~'
                && (relativeUrl[1] == '/' || relativeUrl[1] == '\\')) {
                relativeUrl = relativeUrl.Substring(2);
                foundSlash = true;
            } else foundSlash = false;
            foreach (char c in relativeUrl) {
                if (!foundQM) {
                    if (c == '?') foundQM = true;
                    else {
                        if (c == '/' || c == '\\') {
                            if (foundSlash) continue;
                            else {
                                sbUrl.Append('/');
                                foundSlash = true;
                                continue;
                            }
                        } else if (foundSlash) foundSlash = false;
                    }
                }
                sbUrl.Append(c);
            }

            return sbUrl.ToString();
        }
    }
}

