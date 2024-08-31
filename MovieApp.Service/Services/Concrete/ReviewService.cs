using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Context;
using MovieApp.Data.UnitOfWorks;
using MovieApp.Entity.Dtos.ReviewDtos;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Concrete
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task CreateAsync(ReviewAddDto reviewAddDto, int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());

            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var movieExists = await _unitOfWork.GetRepository<Movie>().AnyAsync(m => m.Id == reviewAddDto.MovieId);
            if (!movieExists)
            {
                throw new Exception("Geçersiz MovieId. Böyle bir film bulunamadı.");
            }

            Review review = new(
                reviewAddDto.Title,
                reviewAddDto.Description,
                reviewAddDto.Rate,
                reviewAddDto.MovieId,
                appUserId

            );

            await _unitOfWork.GetRepository<Review>().AddAsync(review);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<ReviewDto>> GetAllOfFilmAsync(int movieId, int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());
            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var reviews = await _dbContext.Reviews
                .Where(x => x.MovieId == movieId)
                .Include(r => r.AppUser)           
                .Include(r => r.Movie)             
                .ToListAsync();

            var reviewDtos = reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                Title = review.Title,
                Description = review.Description,
                Rate = review.Rate,
                AppUserName = review.AppUser != null ? review.AppUser.UserName : null,
                MovieName = review.Movie.Title,
                AppUserId = appUserId,
                MovieId = movieId
            }).ToList();

            return reviewDtos;
        }


        public async Task<List<ReviewDto>> GetAllByUserAsync(int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());
            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var reviews = await _dbContext.Reviews
                .Where(x => x.AppUserId == appUserId)
                .Include(r => r.AppUser)
                .Include(r => r.Movie)
                .ToListAsync();

            var reviewDtos = reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                Title = review.Title,
                Description = review.Description,
                Rate = review.Rate,
                AppUserName = review.AppUser != null ? review.AppUser.UserName : null,
                MovieName = review.Movie.Title,
                AppUserId = appUserId,
                MovieId = review.Movie.Id
            }).ToList();

            return reviewDtos;
        }
    }
}
