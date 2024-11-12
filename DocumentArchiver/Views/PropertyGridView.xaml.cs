using System.Windows.Controls;

namespace DocumentArchiver.Views
{
    /// <summary>
    /// Interaction logic for PropertyGridView.xaml
    /// </summary>
    public partial class PropertyGridView : UserControl
    {
        private int _errors = 0;

        public PropertyGridView()
        {
            InitializeComponent();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)

                _errors++;
            else

                _errors--;
        }
    }
}