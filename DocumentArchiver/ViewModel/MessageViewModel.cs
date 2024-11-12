using System;
using System.IO;
using DocumentArchiver.Commands;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        public FileInfo[] files;
        private IEventAggregator eventAggregator;
        private string message;
        private DocumentSettings documentSettings;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                RaisePropertyChanged(() => Message);
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

        public MessageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<UpdateMessagesEvent>().Subscribe(UpdateMessages);

            eventAggregator.GetEvent<FinalMessageEvent>().Subscribe(FinalMessage);
            this.documentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;
        }

        private void FinalMessage(string obj)
        {
            var logText = ReadLogFile();
            this.message += Environment.NewLine + logText;
            RaisePropertyChanged(() => Message);
        }

        private void UpdateMessages(string textMessage)
        {
            this.message += Environment.NewLine + textMessage;
            RaisePropertyChanged(() => Message);
        }

        private string ReadLogFile()
        {
            string logFile = Path.Combine(documentSettings.InputFolder, documentSettings.ProjectName.Replace(" ", "") + ".log");
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
    }
}