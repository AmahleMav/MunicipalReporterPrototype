using MunicipalReporterPrototype.Models;
using Microsoft.Win32;
using System;
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
                string appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MunicipalReporter");
                Directory.CreateDirectory(appDir);

                string fileName = Path.GetFileName(openFileDialog.FileName);
                string destPath = Path.Combine(appDir, fileName);
                File.Copy(openFileDialog.FileName, destPath, true);

                attachedFilePath = destPath;
                lblAttachment.Text = $"Attached: {fileName}";
                progressReport.Value = 50;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string location = (cmbLocation.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            string category = (cmbCategory.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            string description = new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text.Trim();

            if (string.IsNullOrEmpty(location) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please fill in all fields before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ServiceIssue issue = new ServiceIssue(location, category, description, attachedFilePath);
            MainWindow.Issues.Add(issue);

            progressReport.Value = 100;
            MessageBox.Show("Issue submitted successfully!",
                            "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            cmbLocation.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Document.Blocks.Clear();
            lblAttachment.Text = "No file attached";
            attachedFilePath = string.Empty;
            progressReport.Value = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
