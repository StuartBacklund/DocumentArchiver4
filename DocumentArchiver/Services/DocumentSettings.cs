using System;
using System.ComponentModel;
using System.IO;
using DocumentArchiver.Helpers;

namespace DocumentArchiver.Services
{
    public class DocumentSettings : IDataErrorInfo, INotifyPropertyChanged, IDocumentSettings
    {
        private string DataDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        private string cssStyleSheet;
        private string tableImage;
        private string viewImage;
        private string procedureImage;
        private string columnImage;
        private string databaseImage;
        private string indexImage;
        private string pKColumnImage;
        private string parameterImage;

        /// <summary>Style Sheet</summary>
        /// <value>String containing the style sheet path.</value>
        [Browsable(true)]
        public string CssStyleSheet
        {
            get
            {
                return cssStyleSheet;
            }
            set
            {
                cssStyleSheet = value;
                RaisePropertyChanged("CssStyleSheet");
            }
        }

        /// <summary>Table Image</summary>
        /// <value>String containing the path to the Table image file.</value>
        [Browsable(true)]
        public string TableImage
        {
            get
            {
                return tableImage;
            }
            set
            {
                tableImage = value;
                RaisePropertyChanged("TableImage");
            }
        }

        /// <summary>View Image</summary>
        /// <value>String containing the path to the View image file.</value>
        [Browsable(true)]
        public string ViewImage
        {
            get
            {
                return viewImage;
            }
            set
            {
                viewImage = value;
                RaisePropertyChanged("ViewImage");
            }
        }

        /// <summary>Stored Procedure Image</summary>
        /// <value>String containing the path to the Stored Procedure image file.</value>
        [Browsable(true)]
        public string ProcedureImage
        {
            get
            {
                return procedureImage;
            }
            set
            {
                procedureImage = value;
                RaisePropertyChanged("ProcedureImage");
            }
        }

        /// <summary>Column Image</summary>
        /// <value>String containing the path to the Column image file.</value>
        [Browsable(true)]
        public string ColumnImage
        {
            get
            {
                return columnImage;
            }
            set
            {
                columnImage = value;
                RaisePropertyChanged("ColumnImage");
            }
        }

        /// <summary>Database Image</summary>
        /// <value>String containing the path to the Database image file.</value>
        [Browsable(true)]
        public string DatabaseImage
        {
            get
            {
                return databaseImage;
            }
            set
            {
                databaseImage = value;
                RaisePropertyChanged("DatabaseImage");
            }
        }

        /// <summary>Index Image</summary>
        /// <value>String containing the path to the Index image file.</value>
        [Browsable(true)]
        public string IndexImage
        {
            get
            {
                return indexImage;
            }
            set
            {
                indexImage = value;
                RaisePropertyChanged("IndexImage");
            }
        }

        /// <summary>Primary Key Column Image</summary>
        /// <value>String containing the path to the Primary Key Column image file.</value>
        [Browsable(true)]
        public string PKColumnImage
        {
            get
            {
                return pKColumnImage;
            }
            set
            {
                pKColumnImage = value;
                RaisePropertyChanged("PKColumnImage");
            }
        }

        /// <summary>Parameter Image</summary>
        /// <value>String containing the path to the Parameter image file.</value>
        [Browsable(true)]
        public string ParameterImage
        {
            get
            {
                return parameterImage;
            }
            set
            {
                parameterImage = value;
                RaisePropertyChanged("ParameterImage");
            }
        }

        private string error = string.Empty;
        private string treeViewStyleSheet;
        private string treeViewJavascript;
        private string treeNodeDotImage;
        private string treeNodeOpenImage;
        private string treeNodeClosedImage;

        /// <summary>MSDN TreeView Style Sheet</summary>
        /// <value>String containing the Tree View style sheet path.</value>
        [Browsable(true)]
        public string TreeViewStyleSheet
        {
            get
            {
                return treeViewStyleSheet;
            }
            set
            {
                treeViewStyleSheet = value;
                RaisePropertyChanged("TreeViewStyleSheet");
            }
        }

        /// <summary>MSDN TreeView Javascript File</summary>
        /// <value>String containing the Tree View javascript path.</value>
        [Browsable(true)]
        public string TreeViewJavascript
        {
            get
            {
                return treeViewJavascript;
            }
            set
            {
                treeViewJavascript = value;
                RaisePropertyChanged("TreeViewJavascript");
            }
        }

        /// <summary>Table Image</summary>
        /// <value>String containing the path to the Tree Node Dot Image file.</value>
        [Browsable(true)]
        public string TreeNodeDotImage
        {
            get
            {
                return treeNodeDotImage;
            }
            set
            {
                treeNodeDotImage = value;
                RaisePropertyChanged("TreeNodeDotImage");
            }
        }

        /// <summary>Table Image</summary>
        /// <value>String containing the path to the TreeNode Opened Image file.</value>
        [Browsable(true)]
        public string TreeNodeOpenImage
        {
            get
            {
                return treeNodeOpenImage;
            }
            set
            {
                treeNodeOpenImage = value;
                RaisePropertyChanged("TreeNodeOpenImage");
            }
        }

        /// <summary>Table Image</summary>
        /// <value>String containing the path to the TreeNode Closed Image file.</value>
        [Browsable(true)]
        public string TreeNodeClosedImage
        {
            get
            {
                return treeNodeClosedImage;
            }
            set
            {
                treeNodeClosedImage = value;
                RaisePropertyChanged("TreeNodeClosedImage");
            }
        }

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                RaisePropertyChanged("ProjectName");
            }
        }

        private string pageName;

        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
                RaisePropertyChanged("PageName");
            }
        }

        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set
            {
                outputFolder = value;
                RaisePropertyChanged("OutputFolder");
            }
        }

        private string modifiedDate;

        public string ModifiedDate
        {
            get { return modifiedDate; }
            set
            {
                modifiedDate = value;
                RaisePropertyChanged("ModifiedDate");
            }
        }

        private string numberOfArticles;

        public string NumberOfArticles
        {
            get { return numberOfArticles; }
            set
            {
                numberOfArticles = value;
                RaisePropertyChanged("NumberOfArticles");
            }
        }

        private string inputFolder;

        public string InputFolder
        {
            get { return inputFolder; }
            set
            {
                inputFolder = value;
                RaisePropertyChanged("InputFolder");
            }
        }

        private string documentName;

        public string DocumentName
        {
            get { return documentName; }
            set
            {
                documentName = value;
                RaisePropertyChanged("DocumentName");
            }
        }

        public DirectoryInfo WorkingDirectory { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DocumentSettings()
        {
        }

        public void Run(string projectName,
                                string pageName,
                                string outputFolder,
                                string modifiedDate,
                                string numberOfArticles,
                                string inputFolder,
                                string documentName)
        {
            this.projectName = projectName;
            this.pageName = pageName;
            this.outputFolder = outputFolder;
            this.modifiedDate = modifiedDate;
            this.numberOfArticles = numberOfArticles;
            this.inputFolder = inputFolder;
            this.documentName = documentName;
        }

        public string Error
        {
            get
            {
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "DocumentName":
                        if (string.IsNullOrEmpty(DocumentName))
                            error = "!";
                        break;

                    case "ProjectName":
                        if (string.IsNullOrEmpty(ProjectName))
                            error = "!";
                        break;

                    default:
                        break;
                }

                return error;
            }
        }

        public void LoadDefaults()
        {
            OutputFolder = Path.Combine(DataDirectory, "Output");

            this.ProjectName = StringExtensionMethods.GetMonthNameAndYear();

            if (File.Exists(Path.Combine(DataDirectory, "Resources", "msdn.css")))
            {
                CssStyleSheet = Path.Combine(DataDirectory, "Resources", "msdn.css");
            }

            if (File.Exists(Path.Combine(DataDirectory, "Resources", "tree.css")))
            {
                TreeViewStyleSheet = Path.Combine(DataDirectory, "Resources", "tree.css");
            }

            if (File.Exists(Path.Combine(DataDirectory, "Resources", "tree.js")))
            {
                TreeViewJavascript = Path.Combine(DataDirectory, "Resources", "tree.js");
            }

            if (File.Exists(Path.Combine(DataDirectory, "Images", "treenodedot.gif")))
                TreeNodeDotImage = Path.Combine(DataDirectory, "Images", "treenodedot.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "treenodeminus.gif")))
                TreeNodeOpenImage = Path.Combine(DataDirectory, "Images", "treenodeminus.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "treenodeplus.gif")))
                TreeNodeClosedImage = Path.Combine(DataDirectory, "Images", "treenodeplus.gif");

            // Defaults for HTML Style sheet and images

            if (File.Exists(Path.Combine(DataDirectory, "Images", "Column.gif")))
                ColumnImage = Path.Combine(DataDirectory, "Images", "Column.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "diskdrive.png")))
                DatabaseImage = Path.Combine(DataDirectory, "Images", "diskdrive.png");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "Index.gif")))
                IndexImage = Path.Combine(DataDirectory, "Images", "Index.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "Parameter.gif")))
                ParameterImage = Path.Combine(DataDirectory, "Images", "Parameter.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "PKColumn.gif")))
                PKColumnImage = Path.Combine(DataDirectory, "Images", "PKColumn.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "Procedure.gif")))
                ProcedureImage = Path.Combine(DataDirectory, "Images", "Procedure.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "Table.gif")))
                TableImage = Path.Combine(DataDirectory, "Images", "Table.gif");

            if (File.Exists(Path.Combine(DataDirectory, "Images", "View.gif")))
                ViewImage = Path.Combine(DataDirectory, "Images", "View.gif");

            var success = FileFunctions.PrepareProjectDirectory(OutputFolder, this);
        }

        public string GetFilePath(string file)
        {
            return Path.Combine(OutputFolder, file);
        }
    }
}