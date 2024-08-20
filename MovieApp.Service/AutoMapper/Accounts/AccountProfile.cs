using AutoMapper;
using MovieApp.Entity.Dtos.AccountDtos;
using MovieApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.AutoMapper.Accounts
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountDto, AppUser>().ReverseMap();
            CreateMap<AccountUpdateDto, AppUser>().ReverseMap();
        }
    }
}
