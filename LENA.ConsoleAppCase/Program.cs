
namespace LENA.ConsoleAppCase
{
    public class Event
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; }
    }

    public class LocationDuration
    {
        public string From { get; set; }
        public string To { get; set; }
        public int DurationMinutes { get; set; }
    }


    class Program
    {
        public static IEnumerable<Event> BasicOrderAlgorithm(IEnumerable<Event> evnts, IEnumerable<LocationDuration> locationDurations)
        {
            var sortedEvents = evnts.OrderByDescending(e => e.Priority).ThenBy(e => e.EndTime).ToList();
            var checkedEvents = new List<Event>();
            var results = new List<Event>();

            TimeSpan lastEndTime = TimeSpan.Zero;
            foreach (var e in sortedEvents)
            {
                if (checkedEvents.Contains(e)) continue;
                if (sortedEvents.IndexOf(e) == 0 )
                {
                    results.Add(e);
                    checkedEvents.Add(e);
                    lastEndTime = e.EndTime;
                    continue;
                }

                var locationDurationFromLast = locationDurations.FirstOrDefault(j => 
                (j.From == results.Last().Location || j.To == e.Location) && 
                (j.From == e.Location || j.To == results.Last().Location),new LocationDuration() { DurationMinutes = 0});

                if (e.StartTime + TimeSpan.FromMinutes(locationDurationFromLast.DurationMinutes) >= lastEndTime || 
                    e.EndTime - TimeSpan.FromMinutes(locationDurationFromLast.DurationMinutes) < results.Min(r => r.StartTime))
                {
                    results.Add(e);
                    checkedEvents.Add(e);
                    lastEndTime = e.EndTime;
                }
            }
            return results;
        }
        static void Main(string[] args)
        {
            var events = new List<Event>
            {
                new Event { Id = 1, StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:00"), Location = "A", Priority = 50 },
                new Event { Id = 2, StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("11:00"), Location = "B", Priority = 30 },
                new Event { Id = 3, StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("12:30"), Location = "A", Priority = 40 },
                new Event { Id = 4, StartTime = TimeSpan.Parse("14:30"), EndTime = TimeSpan.Parse("16:00"), Location = "C", Priority = 70 },
                new Event { Id = 5, StartTime = TimeSpan.Parse("14:25"), EndTime = TimeSpan.Parse("15:30"), Location = "B", Priority = 60 },
                new Event { Id = 6, StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("14:00"), Location = "D", Priority = 80 }
            };
            var locationDurations = new List<LocationDuration>
            {
            new LocationDuration { From = "A", To = "B", DurationMinutes = 15 },
            new LocationDuration { From = "A", To = "C", DurationMinutes = 20 },
            new LocationDuration { From = "A", To = "D", DurationMinutes = 10 },
            new LocationDuration { From = "B", To = "C", DurationMinutes = 5 },
            new LocationDuration { From = "B", To = "D", DurationMinutes = 25 },
            new LocationDuration { From = "C", To = "D", DurationMinutes = 25 }
            };


            var x = BasicOrderAlgorithm(events, locationDurations);
            var maxEvents = x.Count();
            var eventIds = x.Select(q => q.Id);
            var priorities = x.Select(q => q.Priority);

            Console.WriteLine($"Maksimum Katılınabilecek Etkinlik: {maxEvents}");
            Console.WriteLine($"Etkinliklerin Id'leri: {string.Join(", ", eventIds)}");
            Console.WriteLine($"Önemlilik değerleri toplamı: {string.Join(", ", priorities.Sum())}");
            Console.ReadKey();
        }
    }
}