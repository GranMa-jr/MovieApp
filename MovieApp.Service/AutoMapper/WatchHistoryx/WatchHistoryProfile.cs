using AutoMapper;
using MovieApp.Entity.Dtos.WatchHistoryDtos;
using MovieApp.Entity.Entities;

namespace MovieApp.Service.AutoMapper.WatchHistoryx
{
    public class WatchHistoryProfile : Profile
    {
        public WatchHistoryProfile()
        {
            CreateMap<WatchHistoryDto, WatchHistory>().ReverseMap();
            CreateMap<WatchHistoryAddDto, WatchHistory>().ReverseMap();
        }
    }
}