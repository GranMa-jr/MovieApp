using MovieApp.Entity.Entities;

namespace MovieApp.Service.Services.Abstractions
{
    public interface IMovieService
    {
        Task<List<MovieDto>> GetAllAsync();
        Task<Movie> GetById(int id);
        Task CreateAsync(MovieAddDto movieAddDto);
        Task<string> UpdateAsync(MovieUpdateDto movieUpdateDto);
        Task<string> DeleteAsync(int movieId);
    }
}