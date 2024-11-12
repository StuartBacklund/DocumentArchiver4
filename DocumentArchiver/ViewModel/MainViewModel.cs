using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using DocumentArchiver.Commands;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                RaisePropertyChanged(() => ProjectName);
            }
        }

        private string pageName;

        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
                RaisePropertyChanged(() => PageName);
            }
        }

        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set
            {
                outputFolder = value;
                RaisePropertyChanged(() => OutputFolder);
            }
        }

        private string modifiedDate;

        public string ModifiedDate
        {
            get { return modifiedDate; }
            set
            {
                modifiedDate = value;
                RaisePropertyChanged(() => ModifiedDate);
            }
        }

        private string numberOfArticles;

        public string NumberOfArticles
        {
            get { return numberOfArticles; }
            set
            {
                numberOfArticles = value;
                RaisePropertyChanged(() => NumberOfArticles);
            }
        }

        private string inputFolder;

        [Required]
        public string InputFolder
        {
            get { return inputFolder; }
            set
            {
                inputFolder = value;
                RaisePropertyChanged(() => InputFolder);
            }
        }

        private string documentName;

        [Required]
        public string DocumentName
        {
            get { return documentName; }
            set
            {
                documentName = value;
                RaisePropertyChanged(() => DocumentName);
            }
        }

        private DocumentSettings documentSettings;

        public DocumentSettings DocumentSettings
        {
            get { return documentSettings; }
            set
            {
                documentSettings = value;
                RaisePropertyChanged(() => DocumentSettings);
            }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        private string messageBoxText;

        public string MessageBoxText
        {
            get { return messageBoxText; }
            set
            {
                messageBoxText = value;
                RaisePropertyChanged(() => MessageBoxText);
            }
        }

        private string messages;

        public string Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                RaisePropertyChanged(() => Messages);
            }
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.DocumentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;
            eventAggregator.GetEvent<UpdateBusyEvent>().Subscribe(UpdateBusy, ThreadOption.UIThread);
            eventAggregator.GetEvent<CompileDocumentEvent>().Subscribe(CompileDocumentAsync);
        }

        private void CompileDocumentAsync(string obj)
        {
            IsBusy = true;
            RaisePropertyChanged(() => IsBusy);
            UpdateMessages(@"Process staretd..");
            UpdateMessages(@"Please wait..");
            RaisePropertyChanged(() => Messages);

            ArticleCompiler articleCompiler = new ArticleCompiler(this.DocumentSettings);

            GetFilesAsync(articleCompiler);
        }

        public Task GetFilesAsync(ArticleCompiler articleCompiler)
        {
            return Task.Run(() =>
            {
                UpdateMessageBox("Compiler defaults loaded.");
                articleCompiler.Setup();
                UpdateMessageBox("Compiling documents.");
                articleCompiler.CompileProject();
                UpdateMessageBox("Documents compiled.");
                MessageBoxText += articleCompiler.ZipFiles();
                UpdateMessageBox("Documents zipped.");
                Messages += ReadLogFile();
                UpdateMessages(@"Compilation process complete.");
                UpdateMessages(@"Zip archive with Web page container completed.");
                IsBusy = false;
            });
        }

        private string ReadLogFile()
        {
            string logFile = Path.Combine(DocumentSettings.InputFolder, documentSettings.ProjectName.Replace(" ", "") + ".log");
            string line = "Error Opening Log File";

            if (File.Exists(logFile))
            {
                using (StreamReader sr = new StreamReader(logFile))
                {
                    line = sr.ReadToEnd();
                    sr.Close();
                }
            }
            return line;
        }

        private void UpdateMessages(string message)
        {
            Messages += string.Format(message + Environment.NewLine);
            RaisePropertyChanged(() => Messages);
        }

        private void UpdateMessageBox(string message)
        {
            MessageBoxText += string.Format(message + Environment.NewLine);
            RaisePropertyChanged(() => MessageBoxText);
        }

        private void UpdateBusy(bool obj)
        {
            IsBusy = (bool)obj;
            RaisePropertyChanged(() => IsBusy);
        }
    }
}