using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Context;
using MovieApp.Data.UnitOfWorks;
using MovieApp.Entity.Dtos.WatchHistoryDtos;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Concrete
{
    public class WatchHistoryService : IWatchHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public WatchHistoryService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task CreateAsync(WatchHistoryAddDto watchHistoryAddDto, int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());

            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var movieExists = await _unitOfWork.GetRepository<Movie>().AnyAsync(m => m.Id == watchHistoryAddDto.MovieId);
            if (!movieExists)
            {
                throw new Exception("Geçersiz MovieId. Böyle bir film bulunamadı.");
            }

            WatchHistory watchHistory = new(
                watchHistoryAddDto.MovieId,
                appUserId
            );

            await _unitOfWork.GetRepository<WatchHistory>().AddAsync(watchHistory);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<WatchHistoryDto>> GetAllByUserAsync(int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());
            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var watchHistories = await _dbContext.WatchHistories
                .Where(x => x.AppUserId == appUserId) 
                .Include(r => r.AppUser)
                .Include(r => r.Movie)
                .ToListAsync();

            var watchHistoryDtos = watchHistories.Select(watchHistory => new WatchHistoryDto
            {
                Id = watchHistory.Id,
                Title = watchHistory.Movie.Title,
                Description = watchHistory.Movie.Description,
                Rate = watchHistory.Movie.Rate,
                AppUserId = appUserId,
                MovieId = watchHistory.Movie.Id,
                Time = watchHistory.Movie.Time
            }).ToList();

            return watchHistoryDtos;
        }

        public async Task<string> DeleteAsync(int WatchHistoryId, int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());
            if (currentUser == null)
            {
                throw new Exception("Kullan�c� bulunamad�.");
            }

            var watchHistory = await _unitOfWork.GetRepository<WatchHistory>().GetByGuidAsync(WatchHistoryId);

            await _unitOfWork.GetRepository<WatchHistory>().DeleteAsync(watchHistory);
            await _unitOfWork.SaveAsync();

            return watchHistory.Movie.Title;
        }
    }
}
