﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DocumentArchiver.Commands;
using DocumentArchiver.Helpers;
using DocumentArchiver.Services;
using GalaSoft.MvvmLight;
using MahApps.Metro;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Commands;

namespace DocumentArchiver.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private AppTheme lightTheme = new AppTheme("BaseLight", new Uri(string.Format("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml")));
        private AppTheme darkTheme = new AppTheme("BaseDark", new Uri(string.Format("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml")));
        private AppTheme selectedTheme;
        private Accent selectedAccent;
        private IEventAggregator eventAggregator;
        private IUserSettings userSettings;

        public ICommand ChangeThemeCommand
        {
            get
            {
                return new DelegateCommand<object>(this.ChangeTheme);
            }
        }

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

        public SettingsViewModel(IEventAggregator eventAggregator, IUserSettings userSettings)
        {
            this.eventAggregator = eventAggregator;
            this.userSettings = userSettings;
            this.DocumentSettings = App.Current.FindResource("objDocumentSettings") as DocumentSettings;
            accentList = ThemeManager.Accents.ToList();
            themeList = ThemeManager.AppThemes.ToList();
            this.selectedAccent = ThemeManager.GetAccent(userSettings.DefaultAccent);
            this.selectedTheme = ThemeManager.GetAppTheme(userSettings.DefaultTheme);
        }

        private void ChangeAccent()
        {
            var theme = ThemeManager.DetectAppStyle(App.Current);
            ThemeManager.ChangeAppStyle(App.Current, ThemeManager.GetAccent(SelectedAccent.Name), theme.Item1);
            try
            {
                userSettings.Save(SelectedTheme.Name, SelectedAccent.Name, DocumentSettings.InputFolder);
            }
            catch (Exception ex)
            {
                eventAggregator.GetEvent<ShowErrorMessageEvent>().Publish("Unable to save your application settings. Ensure that the application executable has administrator priveledges assigned.");
            }
        }

        private void ChangeTheme(object obj)
        {
            ThemeManager.ChangeAppStyle(App.Current, ThemeManager.GetAccent(SelectedAccent.Name), ThemeManager.GetAppTheme(SelectedTheme.Name));
            try
            {
                userSettings.Save(SelectedTheme.Name, SelectedAccent.Name, DocumentSettings.InputFolder);
            }
            catch (Exception ex)
            {
                eventAggregator.GetEvent<ShowErrorMessageEvent>().Publish("Unable to save your application settings. Ensure that the application executable has administrator priveledges assigned.");
            }
        }

        public AppTheme SelectedTheme
        {
            get
            {
                return selectedTheme;
            }
            set
            {
                if (value.Name != selectedTheme.Name)
                {
                    var newtheme = ThemeManager.GetInverseAppTheme(selectedTheme);
                    ThemeManager.ChangeAppStyle(App.Current, ThemeManager.GetAccent(SelectedAccent.Name), newtheme);
                }

                selectedTheme = value;
                RaisePropertyChanged(() => SelectedTheme);
                App.Current.MainWindow.Activate();
            }
        }

        public Accent SelectedAccent
        {
            get
            {
                return selectedAccent ?? ThemeManager.DetectAppStyle().Item2;
            }
            set
            {
                selectedAccent = value;
                RaisePropertyChanged(() => SelectedAccent);
            }
        }

        private List<Accent> accentList;

        public List<Accent> AccentList
        {
            get
            {
                return accentList;
            }
            set
            {
                accentList = value;
                RaisePropertyChanged(() => AccentList);
            }
        }

        private List<AppTheme> themeList;

        public List<AppTheme> AppThemes
        {
            get
            {
                return themeList;
            }
            set
            {
                themeList = value;
                RaisePropertyChanged(() => AppThemes);
            }
        }
    }
}