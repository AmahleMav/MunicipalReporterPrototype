using MunicipalReporterPrototype.Models;
using System.Windows;

namespace MunicipalReporterPrototype
{
    public partial class MainWindow : Window
    {
        public static IssueLinkedList Issues = new IssueLinkedList();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReportIssue_Click(object sender, RoutedEventArgs e)
        {
            new ReportIssueWindow().Show();
            this.Close();
        }

        private void btnChatBot_Click(object sender, RoutedEventArgs e)
        {
            new ChatBot().ShowDialog();
        }

        private void BtnEvents_Click(object sender, RoutedEventArgs e)
        {
            new Events().Show();
        }

    }
}
