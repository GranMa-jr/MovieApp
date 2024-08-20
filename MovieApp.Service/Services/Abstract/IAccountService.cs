using MovieApp.Entity.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Abstract
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountInfoAsync(int id, int appUserId);
        Task<string> UpdateAsync(AccountUpdateDto accountUpdateDto, int appUserId);
    }
}
