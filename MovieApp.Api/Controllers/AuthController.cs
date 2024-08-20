using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entity.Dtos.AuthDtos;
using MovieApp.Service.Services.Abstract;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                return Ok("Kullanıcı başarıyla kaydoldu.");
            }
            else
            {
                return BadRequest("Kayıt olmadı");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto model)
        {
            var token = await _authService.LoginUserAsync(model);

            if (token != null)
            {
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Geçersiz e-posta veya şifre.");
            }
        }
    }
}
