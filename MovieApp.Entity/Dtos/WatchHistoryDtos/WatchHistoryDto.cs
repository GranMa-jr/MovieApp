using MovieApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Entity.Dtos.WatchHistoryDtos
{
	public class WatchHistoryDto
	{

		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Rate { get; set; }
		public int AppUserId { get; set; }
		public int MovieId { get; set; }
		public double Time { get; set; }
	}
}
