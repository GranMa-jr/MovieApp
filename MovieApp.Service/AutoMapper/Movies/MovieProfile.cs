using AutoMapper;
using MovieApp.Entity.Entities;

namespace MovieApp.Service.AutoMapper.Movies
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
            CreateMap<MovieAddDto, Movie>().ReverseMap();
            CreateMap<MovieUpdateDto, Movie>().ReverseMap();
        }
    }
}