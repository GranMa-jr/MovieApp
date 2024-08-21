using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

            // MovieId'nin geçerli olup olmadığını kontrol edin
            var movieExists = await _unitOfWork.GetRepository<Movie>().AnyAsync(m => m.Id == reviewAddDto.MovieId);
            if (!movieExists)
            {
                throw new Exception("Geçersiz MovieId. Böyle bir film bulunamadı.");
            }

            Review review = new(
                reviewAddDto.Title,
                reviewAddDto.Description,
                reviewAddDto.Rate,
                appUserId,
                reviewAddDto.MovieId
            );

            await _unitOfWork.GetRepository<Review>().AddAsync(review);
            await _unitOfWork.SaveAsync();
        }

        public Task<List<ReviewDto>> GetAllAsync(int appUserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReviewDto>> GetAllByUserAsync(int appUserId)
        {
            throw new NotImplementedException();
        }
    }
}
