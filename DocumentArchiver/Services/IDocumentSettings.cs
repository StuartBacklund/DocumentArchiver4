﻿using System.ComponentModel;
using System.IO;

namespace DocumentArchiver.Services
{
    public interface IDocumentSettings
    {
        string this[string columnName] { get; }

        string ColumnImage { get; }
        string CssStyleSheet { get; }
        string DocumentName { get; set; }
        string Error { get; }
        string InputFolder { get; set; }
        string ModifiedDate { get; set; }
        string NumberOfArticles { get; set; }
        string OutputFolder { get; set; }
        string PageName { get; set; }
        string ProcedureImage { get; }
        string ProjectName { get; set; }
        string TableImage { get; }
        string TreeNodeClosedImage { get; }
        string TreeNodeDotImage { get; }
        string TreeNodeOpenImage { get; }
        string TreeViewJavascript { get; }
        string TreeViewStyleSheet { get; }
        DirectoryInfo WorkingDirectory { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        string GetFilePath(string file);

        void RaisePropertyChanged(string propertyName);

        void Run(string projectName, string pageName, string outputFolder, string modifiedDate, string numberOfArticles, string inputFolder, string documentName);
    }
}