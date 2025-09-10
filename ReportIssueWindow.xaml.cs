using MunicipalReporterPrototype.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace MunicipalReporterPrototype
{
    public partial class ReportIssueWindow : Window
    {
        private string attachedFilePath = string.Empty;

        public ReportIssueWindow()
        {
            InitializeComponent();
        }

        private void btnAttachFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image or Document|*.jpg;*.jpeg;*.png;*.pdf;*.docx;*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                attachedFilePath = openFileDialog.FileName;
                lblAttachment.Text = $"Attached: {Path.GetFileName(attachedFilePath)}";
                progressReport.Value = 50; // half progress when file attached
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string location = txtLocation.Text.Trim();
            string category = (cmbCategory.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            string description = new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text.Trim();

            if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill in all fields before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create ServiceIssue 
            ServiceIssue issue = new ServiceIssue(location, category, description, attachedFilePath);
            MainWindow.Issues.Add(issue);

            progressReport.Value = 100; // mark complete
            MessageBox.Show("Issue submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Reset fields 
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Document.Blocks.Clear();
            lblAttachment.Text = "No file attached";
            attachedFilePath = string.Empty;
            progressReport.Value = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
