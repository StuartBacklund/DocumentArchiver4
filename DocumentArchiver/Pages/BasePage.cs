﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentArchiver.Services;

namespace DocumentArchiver.Pages
{
    public abstract class BasePage
    {
        protected FileStream fs;
        private static string FileExtensionString = ".htm";
        public string DocumentName { get; set; }
        public string FileName { get; set; }

        public string InputFolderPath { get; set; }
        public string pageSection { get; set; }
        public string pathName { get; set; }
        public List<IGrouping<string, FileInfo>> FilesInSection { get; set; }

        private DocumentSettings documentSettings;

        public DocumentSettings DocumentSettings
        {
            get
            {
                return documentSettings;
            }
            set
            {
                documentSettings = value;
            }
        }

        public virtual void WriteHTML()
        {
        }

        public void OpenFile()
        {
            string path = System.IO.Path.Combine(DocumentSettings.InputFolder, FileName);
            fs = new FileStream(path, FileMode.OpenOrCreate);
        }

        public void CloseFile()
        {
            fs.Close();
        }

        public string InputFilePath
        {
            get
            {
                return DocumentSettings.InputFolder;
            }
        }

        public string OutputFilePath
        {
            get
            {
                return DocumentSettings.OutputFolder;
            }
        }

        public string ContentsPage
        {
            get { return string.Concat(DocumentSettings.DocumentName, ".Contents.htm").Replace(" ", ""); }
        }

        public string DefaultPage
        {
            get { return string.Concat(DocumentSettings.DocumentName, ".Default.htm").Replace(" ", ""); }
        }

        public string GetFileName(string fileName)
        {
            return string.Concat(DocumentSettings.DocumentName, ".", fileName, FileExtensionString).Replace(" ", "");
        }

        public string GetFileName(string documentname, string fileName, string fileprefix)
        {
            return string.Concat(documentname, ".", fileprefix, ".", fileName).Replace(" ", "");
        }

        public string GetPagesFileName(string fileName, string fileprefix)
        {
            return string.Concat(DocumentSettings.DocumentName, ".", fileprefix, ".", fileName, FileExtensionString).Replace(" ", "");
        }
    }
}