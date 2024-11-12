using System.IO;
using System.Threading.Tasks;
using DocumentArchiver.Commands;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.ViewModel
{
    public class PropertyGridViewModel : ViewModelBase
    {
        private IEventAggregator eventAggregator;
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
                RaisePropertyChanged(() => DocumentSettings);
            }
        }

        public PropertyGridViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.DocumentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;
            eventAggregator.GetEvent<ListDirectoryContentsEvent>().Subscribe(ListDirectoryContents);
        }

        private void ListDirectoryContents(string workingDirectory)
        {
            var path = new DirectoryInfo(workingDirectory);
            DocumentSettings.InputFolder = path.ToString();
            RaisePropertyChanged(() => DocumentSettings);
        }
    }
}