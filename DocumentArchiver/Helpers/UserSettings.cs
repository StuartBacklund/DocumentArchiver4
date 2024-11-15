﻿using System;
using System.ComponentModel;
using System.Configuration;

namespace DocumentArchiver.Helpers
{
    public interface IUserSettings
    {
        string DefaultAccent { get; set; }
        string DefaultTheme { get; set; }
        string ArticleLocation { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void Save(string theme, string accent, string appFolder = "");

        void Save();

        void Refresh();
    }

    [Serializable]
    public class UserSettings : ApplicationSettingsBase, INotifyPropertyChanged, IUserSettings
    {
        private string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\ArticleApp";
        private string defaultTheme;
        private string defaultAccent;

        public UserSettings()
        {
            LoadSettings();
        }

        [UserScopedSetting()]
        [DefaultSettingValue("BaseLight")]
        public String DefaultTheme
        {
            get { return (String)this["DefaultTheme"]; }
            set { this["DefaultTheme"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Blue")]
        public String DefaultAccent
        {
            get { return (String)(this["DefaultAccent"]); }
            set { this["DefaultAccent"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Articles")]
        public String ArticleLocation
        {
            get { return (String)this["ArticleLocation"]; }
            set { this["ArticleLocation"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool UpperCase
        {
            get { return (bool)(this["UpperCase"]); }
            set { this["UpperCase"] = value; }
        }

        public void Save(string theme, string accent, string appFolder = "")
        {
            try
            {
                defaultAccent = accent;
                defaultTheme = theme;

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DefaultTheme"].Value = theme;
                config.AppSettings.Settings["DefaultAccent"].Value = accent;
                if (!string.IsNullOrEmpty(appFolder))
                {
                    this.ArticleLocation = appFolder;
                    config.AppSettings.Settings["ArticleLocation"].Value = appFolder;
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                base.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(bool appClosing)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["DefaultTheme"].Value = this.DefaultTheme;
            config.AppSettings.Settings["DefaultAccent"].Value = this.DefaultAccent;
            config.AppSettings.Settings["ArticleLocation"].Value = this.ArticleLocation;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            base.Save();
        }

        public void Refresh()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void LoadSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            base.Reload();

            try
            {
                this.DefaultTheme = config.AppSettings.Settings["DefaultTheme"].Value;
                this.DefaultAccent = config.AppSettings.Settings["DefaultAccent"].Value;
                this.ArticleLocation = config.AppSettings.Settings["ArticleLocation"].Value;
            }
            catch (Exception ex)
            {
                this.DefaultTheme = "BaseDark";
                this.DefaultAccent = "Blue";
            }
        }
    }
}