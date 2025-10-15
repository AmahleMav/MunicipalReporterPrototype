using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MunicipalReporterPrototype
{
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += SplashScreen_Loaded;
        }

        private async void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            // Start text animation
            _ = AnimateLoadingText();

            // Wait 3.5 seconds, then open main window
            await Task.Delay(3500);
            new MainWindow().Show();
            this.Close();
        }

        private async Task AnimateLoadingText()
        {
            string baseText = "Loading";
            int dotCount = 0;

            while (IsVisible)
            {
                dotCount = (dotCount + 1) % 4;
                LoadingText.Text = baseText + new string('.', dotCount);
                await Task.Delay(500); 
            }
        }
    }
}
