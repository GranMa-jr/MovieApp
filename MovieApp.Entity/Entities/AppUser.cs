using Microsoft.AspNetCore.Identity;
using MovieApp.Core.Entities;

namespace MovieApp.Entity.Entities
{
    public class AppUser : IdentityUser<int>, IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<WatchHistory> WatchHistories { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}