﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DocumentArchiver.Commands;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Commands;

namespace DocumentArchiver.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        public FileInfo[] files;
        private IEventAggregator eventAggregator;
        private string selectedImagePath;
        private IDocumentSettings documentSettings;
        private bool isnotbusy;

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

        private string navDocumentName;

        public string NavDocumentName
        {
            get { return navDocumentName; }
            set
            {
                if (navDocumentName != value)
                {
                    navDocumentName = value;
                    this.DocumentSettings.DocumentName = value;
                }

                RaisePropertyChanged(() => NavDocumentName);
            }
        }

        public IDocumentSettings DocumentSettings
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

        public ICommand CompileCommand
        {
            get
            {
                return new DelegateCommand<object>(this.Compile);
            }
        }

        public bool IsNotBusy
        {
            get
            {
                return isnotbusy;
            }
            set
            {
                isnotbusy = value;
                RaisePropertyChanged(() => IsNotBusy);
            }
        }

        private void Compile(object obj)
        {
            if (!new DirectoryInfo(DocumentSettings.InputFolder).Exists)
            {
                eventAggregator.GetEvent<ShowErrorMessageEvent>().Publish("Invalid directory selected.");
            }
            else
            {
                IsNotBusy = false;
                Task.Run(() =>
                {
                    eventAggregator.GetEvent<CompileDocumentEvent>().Publish("");
                    IsNotBusy = true;
                });
            }
        }

        public NavigationViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.DocumentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;
            this.NavDocumentName = this.DocumentSettings.DocumentName;
            IsNotBusy = true;
        }

        private void ListDirectoryContents(DirectoryInfo workingDirectory)
        {
        }
    }
}