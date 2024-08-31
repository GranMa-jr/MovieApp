using MovieApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entity.Dtos.WatchHistoryDtos
{
    public class WatchHistoryAddDto
    {
        public int ?AppUserId { get; set; }
        public int MovieId { get; set; }
    }
}
