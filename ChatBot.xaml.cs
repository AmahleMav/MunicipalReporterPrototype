using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MunicipalReporterPrototype
{
    public partial class ChatBot : Window
    {
        public ChatBot()
        {
            InitializeComponent();
            AddBotMessage("Hi there! 🤖 I'm HelpBot, your municipal assistant.\nAsk me about reporting issues, events, or contacting support.");
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string userText = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            // Display user message bubble
            AddUserMessage(userText);
            txtUserInput.Clear();

            // Bot responses
            string lower = userText.ToLower();
            string response = lower switch
            {
                var s when s.Contains("hello") || s.Contains("hi") => "Hello there! 👋 How can I assist you today?",
                var s when s.Contains("how") && s.Contains("report") => "To report an issue, click 'Report Issue' on the main menu and fill in the form.",
                var s when s.Contains("pothole") || s.Contains("broken light") => "Select 'Report Issue' and choose the correct category. You can attach a photo too!",
                var s when s.Contains("status") || s.Contains("update") => "You can view your issue status under 'Service Request Status' in the main menu.",
                var s when s.Contains("event") || s.Contains("announcement") => "Check 'Local Events & Announcements' for the latest community updates.",
                var s when s.Contains("contact") || s.Contains("email") => "You can reach municipal support at support@municipality.gov.za or call 012 345 6789.",
                var s when s.Contains("thank") => "You're welcome! 😊 Always happy to help.",
                var s when s.Contains("bye") || s.Contains("goodbye") => "Goodbye! Stay safe and keep your community clean 🌍.",
                _ => "Thank you for your message! I'll forward it to the municipal team for review."
            };

            AddBotMessage(response);
        }

        // User message bubble 
        private void AddUserMessage(string message)
        {
            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(52, 152, 219)), 
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(60, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Child = new TextBlock
                {
                    Text = message,
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14
                }
            };
            ChatPanel.Children.Add(bubble);
            ChatScroll.ScrollToEnd();
        }

        // Bot message bubble
        private void AddBotMessage(string message)
        {
            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(230, 230, 230)), 
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10, 5, 60, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Child = new TextBlock
                {
                    Text = $"HelpBot: {message}",
                    Foreground = Brushes.Black,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14
                }
            };
            ChatPanel.Children.Add(bubble);
            ChatScroll.ScrollToEnd();
        }

        private void txtUserInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (PlaceholderText == null) return;
            PlaceholderText.Visibility = string.IsNullOrWhiteSpace(txtUserInput.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

    }
}
