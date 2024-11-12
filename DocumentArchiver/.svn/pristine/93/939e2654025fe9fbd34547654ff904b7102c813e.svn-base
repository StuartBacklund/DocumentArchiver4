using System.Windows;
using System.Windows.Controls;
using DocumentArchiver.Commands;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.Views
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : UserControl
    {
        private IEventAggregator eventAggregator;

        public MessageView()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            if (eventAggregator == null)
            {
                eventAggregator = SimpleIoc.Default.GetInstance<IEventAggregator>();
                eventAggregator.GetEvent<UpdateBusyEvent>().Subscribe(UpdateBusy);
            }
        }

        private void UpdateBusy(bool isVisible)
        {
            if (isVisible)
            {
                waiting.Visibility = Visibility.Visible;
            }
            else
            {
                waiting.Visibility = Visibility.Collapsed;
            }
        }
    }
}