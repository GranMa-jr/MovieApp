using AutoMapper;
using MovieApp.Entity.Dtos.ReviewDtos;
using MovieApp.Entity.Entities;

namespace MovieApp.Service.AutoMapper.Reviews
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewDto, Review>().ReverseMap();
            CreateMap<ReviewAddDto, Review>().ReverseMap();
        }
    }
}