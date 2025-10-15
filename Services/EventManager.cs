using MunicipalReporterPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalReporterPrototype.Services
{
    public static class EventManager
    {
        private static readonly List<Event> _events = new();

        public static readonly SortedDictionary<DateTime, List<Event>> EventsByDate = new();
        public static readonly Dictionary<string, List<Event>> EventsByCategory = new();
        public static readonly Queue<Event> NotificationQueue = new();
        public static readonly Stack<Event> RecentlyViewed = new();
        public static readonly HashSet<string> CategorySet = new();

        private static readonly Dictionary<string, int> _searchCounts = new();

        static EventManager() => Seed();

        private static void Seed()
        {
            var samples = new List<Event>
            {
                new("Water Outage Notice", "Utilities", new(2025,10,18), "Umbilo", "Scheduled pipe maintenance in Umbilo."),
                new("Community Clean-Up", "Community", new(2025,10,20), "Glenwood", "Join our Glenwood park cleanup drive."),
                new("Food Safety Workshop", "Health", new(2025,10,25), "Durban Central", "Vendor hygiene training for local food stalls."),
                new("Youth Sports Day", "Recreation", new(2025,10,30), "Westville", "Sports activities for all youth at Westville grounds."),
                new("Municipal Open Day", "Public", new(2025,11,02), "Musgrave", "Meet municipal leaders and learn about local initiatives."),
                new("Farmers Market", "Community", new(2025,11,05), "Berea", "Fresh produce and crafts from Berea farmers."),
                new("Power Maintenance", "Utilities", new(2025,11,10), "Phoenix", "Sub-station upgrade and scheduled power outage."),
                new("Health Check Camp", "Health", new(2025,11,15), "Mayville", "Free community health screening and advice."),
                new("Traffic Diversion", "Roads", new(2025,11,18), "Chatsworth", "Roadworks causing temporary traffic diversion."),
                new("Tree Planting Drive", "Environment", new(2025,11,22), "Reservoir Hills", "Community greening and tree planting event."),
                new("Holiday Parade", "Recreation", new(2025,12,01), "Morningside", "Annual festive parade and local performances."),
                new("Water Quality Report", "Utilities", new(2025,12,05), "Durban North", "Annual report on Durban North water quality."),
                new("Job Fair", "Public", new(2025,12,10), "Overport", "Employment opportunities and career guidance."),
                new("Fire Safety Demo", "Health", new(2025,12,14), "Glenmore", "Fire safety awareness demonstration and training."),
                new("Tech Innovation Expo", "Public", new(2025,12,20), "Umlazi", "Showcase of municipal technology and startups.")
            };


            foreach (var ev in samples) AddEvent(ev);
        }

        public static void AddEvent(Event ev)
        {
            _events.Add(ev);
            CategorySet.Add(ev.Category);

            if (!EventsByCategory.ContainsKey(ev.Category))
                EventsByCategory[ev.Category] = new List<Event>();
            EventsByCategory[ev.Category].Add(ev);

            if (!EventsByDate.ContainsKey(ev.Date.Date))
                EventsByDate[ev.Date.Date] = new List<Event>();
            EventsByDate[ev.Date.Date].Add(ev);

            NotificationQueue.Enqueue(ev);
        }

        public static IEnumerable<Event> GetAll() => _events.OrderBy(e => e.Date);

        public static IEnumerable<Event> Search(string category, DateTime? from, DateTime? to)
        {
            if (!string.IsNullOrEmpty(category))
            {
                if (!_searchCounts.ContainsKey(category)) _searchCounts[category] = 0;
                _searchCounts[category]++;
            }

            IEnumerable<Event> query = _events;

            if (!string.IsNullOrEmpty(category))
                query = query.Where(e => e.Category == category);
            if (from.HasValue)
                query = query.Where(e => e.Date >= from.Value);
            if (to.HasValue)
                query = query.Where(e => e.Date <= to.Value);

            return query.OrderBy(e => e.Date);
        }

        public static IEnumerable<Event> Recommend()
        {
            if (_searchCounts.Count == 0) return Enumerable.Empty<Event>();
            string fav = _searchCounts.OrderByDescending(kv => kv.Value).First().Key;
            return _events.Where(e => e.Category == fav && e.Date >= DateTime.Today)
                          .OrderBy(e => e.Date)
                          .Take(5);
        }

        public static Event ProcessNextNotification()
            => NotificationQueue.Count > 0 ? NotificationQueue.Dequeue() : null;

        public static void AddToRecentlyViewed(Event ev)
        {
            if (ev != null) RecentlyViewed.Push(ev);
        }
    }
}
