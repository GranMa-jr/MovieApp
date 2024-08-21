using MovieApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entity.Entities
{
    public class Review : EntityBase
    {
        public Review() { }

        public Review(string title, string description, int rate, int movieId, int appUserId)
        {
            Title = title;
            Description = description;
            Rate = rate;
            MovieId = movieId;
            AppUserId = appUserId;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
