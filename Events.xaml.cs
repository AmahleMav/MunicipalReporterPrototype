using MunicipalReporterPrototype.Models;
using AppEventManager = MunicipalReporterPrototype.Services.EventManager;

using System;
using System.Linq;
using System.Windows;

namespace MunicipalReporterPrototype
{
    public partial class Events : Window
    {
        public Events()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            CategoryBox.Items.Clear();
            CategoryBox.Items.Add("All");
            foreach (var cat in AppEventManager.CategorySet.OrderBy(c => c))
                CategoryBox.Items.Add(cat);
            CategoryBox.SelectedIndex = 0;

            EventGrid.ItemsSource = AppEventManager.GetAll();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string category = CategoryBox.SelectedItem?.ToString() == "All" ? null : CategoryBox.SelectedItem?.ToString();
            DateTime? from = FromDate.SelectedDate;
            DateTime? to = ToDate.SelectedDate;
            EventGrid.ItemsSource = AppEventManager.Search(category, from, to);
        }

        private void SortBtn_Click(object sender, RoutedEventArgs e)
        {
            var sorted = AppEventManager.GetAll().OrderBy(ev => ev.Title).ToList();
            EventGrid.ItemsSource = sorted;
        }

        private void EventGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (EventGrid.SelectedItem is Event ev)
            {
                AppEventManager.AddToRecentlyViewed(ev);
                MessageBox.Show($"{ev.Title}\n\n{ev.Description}\n\nLocation: {ev.Location}\nDate: {ev.Date:yyyy-MM-dd}",
                                "Event Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ProcessBtn_Click(object sender, RoutedEventArgs e)
        {
            var next = AppEventManager.ProcessNextNotification();
            string msg = next == null
                ? "No pending notifications."
                : $"Notification processed for {next.Title} ({next.Date:yyyy-MM-dd})";
            MessageBox.Show(msg, "Notifications", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RecommendBtn_Click(object sender, RoutedEventArgs e)
        {
            var recs = AppEventManager.Recommend().ToList();
            if (recs.Count == 0)
            {
                MessageBox.Show("No recommendations yet – search a few categories first!", "Recommendations");
                return;
            }

            string msg = string.Join("\n", recs.Select(r => $"{r.Date:yyyy-MM-dd} - {r.Title}"));
            MessageBox.Show($"Based on your searches, you might like:\n\n{msg}", "Recommended Events");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
