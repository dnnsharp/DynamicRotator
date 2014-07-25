using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using DnnSharp.DynamicRotator.Core.Settings;

namespace DnnSharp.DynamicRotator.Core.Services
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
        public string RootName { get; private set; }
        public string RootPhysicalPath { get; private set; }
        public FileBrowser_Folder RootFolder { get; private set; }

        Dictionary<string, bool> _ext;

        public FileBrowser(string rootPhysicalPath, string rootName, params string[] extensions)
        {
            RootPhysicalPath = rootPhysicalPath.ToLower().Replace('/', '\\');
            RootName = rootName;

            _ext = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (string ext in extensions) {
                _ext[ext] = true;
            }

            RootFolder = new FileBrowser_Folder();
            RootFolder.PhysicalPath = RootPhysicalPath;
            RootFolder.RelativePath = RootPhysicalPath.Replace(HttpRuntime.AppDomainAppPath.ToLower(), "").Replace('\\', '/');
            RootFolder.Url = (HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath.TrimEnd('/')) + "/" + RootFolder.RelativePath.Trim('/');
            if (RootFolder.Url == "/")
                RootFolder.Url = "";
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
                file.RelativePath = "/" + filePath.ToLower().Replace(RootPhysicalPath, "").Replace('\\', '/').Trim('/');
                file.Url = ResolveUrl(file.RelativePath);
                files.Add(file);
            }

            return files;
        }

        public void Upload(string relPath, HttpPostedFile file)
        {
            var folderPath = Path.Combine(RootFolder.PhysicalPath, relPath.TrimStart('/'));
            var filePath = Path.Combine(folderPath, file.FileName);
            //if (File.Exists(filePath))
            //    File.Delete(filePath);
            file.SaveAs(filePath);
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

