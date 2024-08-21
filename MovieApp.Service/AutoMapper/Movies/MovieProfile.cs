using AutoMapper;
using MovieApp.Entity.Entities;

namespace MovieApp.Service.AutoMapper.Movies
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
            CreateMap<MovieAddDto, Movie>().ReverseMap();
            CreateMap<MovieUpdateDto, Movie>().ReverseMap();
        }
    }
}