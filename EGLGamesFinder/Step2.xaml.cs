using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EGLGamesFinder
{
    /// <summary>
    /// Interakční logika pro Step2.xaml
    /// </summary>
    public partial class Step2 : Page
    {
        private Frame Step;
        public int AddedCount
        {
            set
            {
                Message.Content = "Done! We added " + value + " items";
            }
        }

        public Step2(Frame step)
        {
            InitializeComponent();
            Step = step;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Repeat_Click(object sender, RoutedEventArgs e)
        {
            Step.Content = MainWindow.step1;
            Message.Content = "Waiting on report";
        }
    }
}
