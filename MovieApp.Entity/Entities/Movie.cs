using MovieApp.Core.Entities;

namespace MovieApp.Entity.Entities
{
    public class Movie : EntityBase
    {
        public Movie()
        {
            
        }

        public Movie(string title, string description, string year, double time, int rate)
        {
            Title = title;
            Description = description;
            Year = year;
            Time = time;
            Rate = rate;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public double Time { get; set; }
        public int Rate { get; set; }

        public ICollection<WatchHistory> WatchHistories { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}