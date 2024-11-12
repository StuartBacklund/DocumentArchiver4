using System;
using System.IO;
using System.Linq;
using DocumentArchiver.Commands;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.ViewModel
{
    public class FilesFolderViewModel : ViewModelBase
    {
        private FileInfo[] files;
        private DocumentSettings documentSettings;
        private IEventAggregator eventAggregator;

        public FileInfo[] Files
        {
            get { return files; }
            set
            {
                files = value;
                RaisePropertyChanged(() => Files);
            }
        }

        public DocumentSettings DocumentSettings
        {
            get
            {
                return documentSettings;
            }
            set
            {
                documentSettings = value;
                RaisePropertyChanged(() => DocumentSettings);
            }
        }

        public FilesFolderViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.DocumentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;
            eventAggregator.GetEvent<ListDirectoryContentsEvent>().Subscribe(ListDirectoryContents);
            if (!string.IsNullOrEmpty(this.DocumentSettings.InputFolder))
            {
                ListDirectoryContents(this.DocumentSettings.InputFolder);
            }
        }

        private void ListDirectoryContents(string workingDirectory)
        {
            var path = new DirectoryInfo(workingDirectory);
            if (path.Exists)
            {
                files = path.GetFiles().Where(x => x.Extension == ".htm").OrderBy(n => n.Name).ToArray();
                this.DocumentSettings.NumberOfArticles = files.Count().ToString();
                this.DocumentSettings.ModifiedDate = DateTime.Today.ToShortDateString();
            }

            RaisePropertyChanged(() => DocumentSettings);
            RaisePropertyChanged(() => Files);
        }
    }
}