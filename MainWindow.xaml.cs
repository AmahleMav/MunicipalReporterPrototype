using MunicipalReporterPrototype.Models;
using System.Windows;

namespace MunicipalReporterPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Static linked list to store issues
        public static IssueLinkedList Issues = new IssueLinkedList();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReportIssue_Click(object sender, RoutedEventArgs e)
        {
            ReportIssueWindow reportIssueWindow = new ReportIssueWindow();
            reportIssueWindow.Show();
            this.Close();
        }
    }
}