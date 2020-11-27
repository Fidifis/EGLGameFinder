using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EGLGamesFinder
{
    /// <summary>
    /// Interakční logika pro Failed.xaml
    /// </summary>
    public partial class Failed : Page
    {
        private Frame Step;

        public Failed(Frame step)
        {
            InitializeComponent();
            Step = step;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Step.Content = MainWindow.step1;
        }

        public void ErrorMessageToShow(string msg)
        {
            ErrorMessage.Text = msg;
        }
    }
}
