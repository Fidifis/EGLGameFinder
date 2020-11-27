using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EGLGamesFinder
{
    /// <summary>
    /// Interakční logika pro Step1.xaml
    /// </summary>
    public partial class Step1 : Page
    {
        private Frame Step;

        public Step1(Frame step)
        {
            InitializeComponent();
            Step = step;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.RemoveBackEntry();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ManifestGenerator generator = new ManifestGenerator();
            bool success = generator.GenerateManifests(Path.Text);

            if (success) {
                Step.Content = MainWindow.step2;
                MainWindow.step2.AddedCount = generator.Addeditems;
            }
            else
            {
                Step.Content = MainWindow.failed;
                MainWindow.failed.ErrorMessageToShow(generator.ErrorMessage);
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Path.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
