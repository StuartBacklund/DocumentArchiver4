﻿using System.Windows;
using DocumentArchiver.Commands;
using DocumentArchiver.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void ChangeAccent(object obj)
        {
            var theme = ThemeManager.DetectAppStyle(App.Current);
            ThemeManager.ChangeAppStyle(App.Current, ThemeManager.GetAccent(obj.ToString()), theme.Item1);
        }

        private void ChangeTheme(object obj)
        {
            var theme = ThemeManager.DetectAppStyle(App.Current);
            var apptheme = ThemeManager.GetInverseAppTheme(theme.Item1);
            ThemeManager.ChangeAppStyle(App.Current, theme.Item2, apptheme);
            Application.Current.MainWindow.Activate();
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            settingsFlyout.IsOpen = !settingsFlyout.IsOpen;
        }

        private async void ShowErrorMessageDialog(string message)
        {
            MessageDialogResult messageResult = await this.ShowMessageAsync("APPLICATION ERROR", message, MessageDialogStyle.Affirmative);
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void Child01_OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}