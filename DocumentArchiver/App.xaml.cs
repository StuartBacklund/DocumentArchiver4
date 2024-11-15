﻿using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DocumentArchiver.Helpers;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using MahApps.Metro;

namespace DocumentArchiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }

        public static string CurrentUser { get; private set; }
        private IUserSettings usersettings;
        private DocumentSettings documentSettings;

        static App()
        {
            var ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var splashScreen = new DocumentArchiver.Views.SplashWindow();
            this.MainWindow = splashScreen;
            splashScreen.Show();

            Task.Factory.StartNew(() =>
            {
                BootStrapper.Instance.Bootstrap(this, e);

                usersettings = SimpleIoc.Default.GetInstance<IUserSettings>();
                documentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;

                documentSettings.InputFolder = usersettings.ArticleLocation != null ? usersettings.ArticleLocation : "";
                documentSettings.LoadDefaults();
                documentSettings.DocumentName = StringExtensionMethods.GetMonthNameAndYear();
                string defaultAccent = usersettings.DefaultAccent ?? "Blue";

                var accent = new Accent(defaultAccent, new Uri(string.Format("pack://application:,,,/MahApps.Metro;component/Styles/Accents/{0}.xaml", defaultAccent)));
                string defaultTheme = usersettings.DefaultTheme ?? "BaseLight";
                ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent(defaultAccent),
                                     ThemeManager.GetAppTheme(defaultTheme));
                CurrentUser = WindowsIdentity.GetCurrent().Name ?? "USERNAME";

                DispatcherHelper.UIDispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    this.MainWindow = mainWindow;
                    mainWindow.Show();
                    splashScreen.Close();
                });
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            usersettings.Save();
            BootStrapper.Instance.ShutDown(this, e);
            base.OnExit(e);
            Application.Current.Shutdown(0);
        }

        private void ExitApp(object sender, ExitEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
    }
}