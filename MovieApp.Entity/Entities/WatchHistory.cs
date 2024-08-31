using MovieApp.Core.Entities;

namespace MovieApp.Entity.Entities
{
    public class WatchHistory : EntityBase
    {
        public WatchHistory()
        {

        }
        public WatchHistory(int appUserId, int movieId)
        {
            AppUserId = appUserId;
            MovieId = movieId;
        }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}