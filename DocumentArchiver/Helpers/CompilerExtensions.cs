using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentArchiver.Helpers
{
    public static class CompilerExtensions
    {
        public static string ReturnFullPathWithFile(string filePath, string fileName)
        {
            return string.Concat(System.IO.Path.Combine(filePath, fileName)).Replace(" ", "");
        }

        public static string ReturnFormattedFileName(this string documentName, string pageName)
        {
            return string.Concat(documentName, ".", pageName).Replace(" ", "");
        }
        public static string ReturnHtmlSectionToCreate(string documentName, string sectionName)
        {
            return string.Format("{0}.{1}.htm", documentName, sectionName).Replace(" ", "");
        }
        public static string ReturnHtmlPageToCreate(string documentName, string sectionName, string fileName)
        {
            return string.Format("{0}.{1}.{2}", documentName, sectionName, fileName).Replace(" ", "");
        }
    }
}