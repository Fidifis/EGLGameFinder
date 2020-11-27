using System.Windows;

namespace EGLGamesFinder
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Step1 step1;
        public static Step2 step2;
        public static Failed failed;

        public MainWindow()
        {
            InitializeComponent();
            step1 = new Step1(Step);
            step2 = new Step2(Step);
            failed = new Failed(Step);
            Step.Content = step1;
        }
    }
}
