using Microsoft.AspNetCore.Identity;
using MovieApp.Entity.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services.Abstract
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDto registerDto);
        Task<string> LoginUserAsync(LoginDto loginDto);
    }
}
