using MovieApp.Entity.Dtos.ReviewDtos;
using MovieApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Abstract
{
    public interface IReviewService
    {
        Task CreateAsync(ReviewAddDto reviewAddDto, int appUserId);
        Task<List<ReviewDto>> GetAllOfFilmAsync(int movieId,int appUserId);
        Task<List<ReviewDto>> GetAllByUserAsync(int appUserId);
    }
}
