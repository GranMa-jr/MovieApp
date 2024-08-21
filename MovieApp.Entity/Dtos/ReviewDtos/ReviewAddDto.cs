using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entity.Dtos.ReviewDtos
{
    public class ReviewAddDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public int MovieId { get; set; }
        public int AppUserId { get; set; }
    }
}
