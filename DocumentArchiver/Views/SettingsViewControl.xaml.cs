using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DocumentArchiver.Helpers;

namespace DocumentArchiver.Views
{
    /// <summary>
    /// Interaction logic for SettingsViewControl.xaml
    /// </summary>
    public partial class SettingsViewControl : UserControl
    {
        public SettingsViewControl()
        {
            InitializeComponent();
        }

        private void RefreshINotify()
        {
            UserSettings opts = (UserSettings)((ObjectDataProvider)
             Application.Current.FindResource("odpSettings")).ObjectInstance;
            opts.Refresh();
        }

        private void RefreshControls()
        {
            UserSettings opts = (UserSettings)((ObjectDataProvider)
                Application.Current.
                    FindResource("odpSettings")).ObjectInstance;

            opts.Refresh();

            cmbThemes.GetBindingExpression(
                ComboBox.SelectedItemProperty).UpdateTarget();
            cmbAccent.GetBindingExpression(
                 ComboBox.SelectedItemProperty).UpdateTarget();
        }

        private void RefreshDataProvider()
        {
            ObjectDataProvider dp =
            (ObjectDataProvider)Application.Current.
                 FindResource("odpSettings");
            UserSettings opts = (UserSettings)dp.ObjectInstance;
            opts.Refresh();
            dp.Refresh();
        }
    }
}