using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BuildHtmlPages
{
    class Program
    {
        static string rootTestDir;

        static void Main(string[] args)
        {
            rootTestDir = args[0];

            if (!Directory.Exists(rootTestDir)) {
                throw new ArgumentException("Directory does not exist: " + rootTestDir);
            }
           
            // iterate all test folders
            StringBuilder sbMainToc = new StringBuilder();
            foreach (string testDir in Directory.GetDirectories(rootTestDir)) {
                if (Path.GetFileName(testDir)[0] == '.')
                    continue;

                sbMainToc.AppendFormat("<a href='{0}/index.html' target='testContents'>{0}</a>\n", Path.GetFileName(testDir));

                BuildHtmlForTestSuite(testDir);
            }

            // write main TOC
            string strMainTocHtml = File.ReadAllText(Path.Combine(rootTestDir, ".build-html/BuildHtmlPages/Templates/toc-main.html"))
                .Replace("{{LinkList}}", sbMainToc.ToString());

            File.WriteAllText(Path.Combine(rootTestDir, "toc.html"), strMainTocHtml);
        }

        static void BuildHtmlForTestSuite(string testSuiteDir)
        {
            // generate main index.html
            string strMainIndexHtml = File.ReadAllText(Path.Combine(rootTestDir, ".build-html/BuildHtmlPages/Templates/index-test-suite.html"))
                .Replace("{{TestSuiteName}}", Path.GetFileName(testSuiteDir));

            File.WriteAllText(Path.Combine(testSuiteDir, "index.html"), strMainIndexHtml);

            // generate toc
            StringBuilder sbToc = new StringBuilder();
            foreach (string testDir in Directory.GetDirectories(testSuiteDir)) {
                if (Path.GetFileName(testDir)[0] == '.')
                    continue;

                sbToc.AppendFormat("<a href='{0}/index.html' target='test'>{0}</a>\n", Path.GetFileName(testDir));
            }

            // write main TOC
            string strMainTocHtml = File.ReadAllText(Path.Combine(rootTestDir, ".build-html/BuildHtmlPages/Templates/toc-test-suite.html"))
                .Replace("{{LinkList}}", sbToc.ToString());

            File.WriteAllText(Path.Combine(testSuiteDir, "toc.html"), strMainTocHtml);
        }
    }
}
