using MovieApp.Entity.Entities;

namespace MovieApp.Service.Services.Abstractions
{
    public interface IMovieService
    {
        Task<List<MovieDto>> GetAllAsync(int appUserId);
        Task<Movie> GetById(int id, int appUserId);
        Task CreateAsync(MovieAddDto movieAddDto, int appUserId);
        Task<string> UpdateAsync(MovieUpdateDto movieUpdateDto, int appUserId);
        Task<string> DeleteAsync(int movieId, int appUserId);
    }
}