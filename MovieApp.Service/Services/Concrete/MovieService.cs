using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using MovieApp.Data.Context;
using MovieApp.Data.UnitOfWorks;
using MovieApp.Entity.Entities;
using MovieApp.Service.Extensions;
using MovieApp.Service.Services.Abstractions;

namespace MovieApp.Service.Services.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly ApplicationDbContext _dbContext;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _dbContext = dbContext;
        }

        public async Task CreateAsync(MovieAddDto movieAddDto)
        {
            Movie movie = new(
                movieAddDto.Title,
                movieAddDto.Description,
                movieAddDto.Year,
                movieAddDto.Time,
                movieAddDto.Rate

                );
            await _unitOfWork.GetRepository<Movie>().AddAsync(movie);
            await _unitOfWork.SaveAsync();
        }

        public async Task<string> DeleteAsync(int movieId)
        {
            var movie = await _unitOfWork.GetRepository<Movie>().GetByGuidAsync(movieId);

            await _unitOfWork.GetRepository<Movie>().DeleteAsync(movie);
            await _unitOfWork.SaveAsync();

            return movie.Title;
        }

        public async Task<List<MovieDto>> GetAllAsync()
        {
            var movies = await _unitOfWork.GetRepository<Movie>().GetAllAsync();
            var map = _mapper.Map<List<MovieDto>>(movies);

            return map;
        }

        public async Task<Movie> GetById(int id)
        {
            var movie = await _unitOfWork.GetRepository<Movie>().GetByGuidAsync(id);
            return movie;
        }

        public async Task<string> UpdateAsync(MovieUpdateDto movieUpdateDto)
        {
            var movie = await _unitOfWork.GetRepository<Movie>().GetAsync(x => x.Id == movieUpdateDto.Id);

            movie.Title = movieUpdateDto.Title;
            movie.Description = movieUpdateDto.Description;
            movie.Year = movieUpdateDto.Year;
            movie.Time = movieUpdateDto.Time;
            movie.Rate = movieUpdateDto.Rate;
            movie.ModifiedDate = DateTime.Now;


            await _unitOfWork.GetRepository<Movie>().UpdateAsync(movie);
            await _unitOfWork.SaveAsync();


            return movie.Title;
        }
    }
}