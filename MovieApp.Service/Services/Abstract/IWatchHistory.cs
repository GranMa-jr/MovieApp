using MovieApp.Entity.Dtos.WatchHistoryDtos;
using MovieApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Abstract
{
    public interface IWatchHistoryService
    {
        Task CreateAsync(WatchHistoryAddDto watchHistoryAddDto, int appUserId);
        Task<List<WatchHistoryDto>> GetAllByUserAsync(int appUserId);
		Task<string> DeleteAsync(int WatchHistoryId, int appUserId);
    }
}
