using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieApp.Data.Context;
using MovieApp.Data.UnitOfWorks;
using MovieApp.Entity.Dtos.AccountDtos;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<AccountDto> GetAccountInfoAsync(int id, int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());

            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var profile = await _unitOfWork.GetRepository<AppUser>().GetByGuidAsync(id);
            var map = _mapper.Map<AccountDto>(profile);

            return map;
        }

        public async Task<string> UpdateAsync(AccountUpdateDto accountUpdateDto, int appUserId)
        {
            var currentUser = await _userManager.FindByIdAsync(appUserId.ToString());
            if (currentUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var account = await _unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == accountUpdateDto.Id);
            if (account == null)
            {
                throw new Exception("Güncellenecek hesap bulunamadı.");
            }

            account.FirstName = accountUpdateDto.FirstName;
            account.LastName = accountUpdateDto.LastName;
            account.Email = accountUpdateDto.Email;

            if (!string.IsNullOrEmpty(accountUpdateDto.Password))
            {
                var passwordHasher = new PasswordHasher<AppUser>();
                account.PasswordHash = passwordHasher.HashPassword(account, accountUpdateDto.Password);
            }

            await _unitOfWork.GetRepository<AppUser>().UpdateAsync(account);
            await _unitOfWork.SaveAsync();

            return account.Email;
        }
    }
}
