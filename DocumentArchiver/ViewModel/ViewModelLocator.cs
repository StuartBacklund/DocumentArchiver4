using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DocumentArchiver.ViewModel
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<MainViewModel>();
                SimpleIoc.Default.Register<FilesFolderViewModel>();
                SimpleIoc.Default.Register<DirectoryViewModel>();
                SimpleIoc.Default.Register<NavigationViewModel>();
                SimpleIoc.Default.Register<PropertyGridViewModel>();
                // SimpleIoc.Default.Register<MessageViewModel>();
                SimpleIoc.Default.Register<SettingsViewModel>();
            }
        }

        public MainViewModel MainView
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingsViewModel SettingsView
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public PropertyGridViewModel PropertyGridView
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<PropertyGridViewModel>();
            }
        }

        /*  public MessageViewModel MessageView
          {
              get
              {
                  return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<MessageViewModel>();
              }
          }*/

        public NavigationViewModel NavigationView
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<NavigationViewModel>();
            }
        }

        public FilesFolderViewModel FileFolderView
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<FilesFolderViewModel>();
            }
        }

        public DirectoryViewModel DirectoryView
        {
            get
            {
                return Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<DirectoryViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}