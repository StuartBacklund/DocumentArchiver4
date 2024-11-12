using System.IO;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.ViewModel
{
    public class DirectoryViewModel : ViewModelBase
    {
        public FileInfo[] files;
        private IEventAggregator eventAggregator;
        private string selectedImagePath;

        public string SelectedImagePath
        {
            get
            {
                return selectedImagePath;
            }

            set
            {
                selectedImagePath = value;
                RaisePropertyChanged(() => SelectedImagePath);
            }
        }

        public DirectoryViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }
    }
}