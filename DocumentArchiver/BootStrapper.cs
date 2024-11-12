using DocumentArchiver.Helpers;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace DocumentArchiver
{
    public class BootStrapper
    {
        private static BootStrapper _instance;

        private BootStrapper()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IUserSettings, UserSettings>();
            SimpleIoc.Default.Register<IEventAggregator, EventAggregator>();
            SimpleIoc.Default.Register<IDocumentSettings, DocumentSettings>();
        }

        public static BootStrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BootStrapper();
                }
                return (_instance);
            }
        }

        public void Bootstrap(App app, System.Windows.StartupEventArgs e)
        {
            //Do bootstap here
        }

        public void ShutDown(App app, System.Windows.ExitEventArgs e)
        {
            //Do shutdown cleanup here
        }
    }
}