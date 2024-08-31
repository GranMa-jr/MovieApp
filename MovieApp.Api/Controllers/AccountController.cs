using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entity.Dtos.AccountDtos;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstract;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetAccountInfoAsync()
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                var profile = await _accountService.GetAccountInfoAsync(appUserId,appUserId);

                return Ok(profile);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Kullanıcı bulunamadı.")
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] AccountUpdateDto accountUpdateDto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                await _accountService.UpdateAsync(accountUpdateDto, appUserId);

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Kullanıcı bulunamadı.")
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }
                else if (ex.Message == "Güncellenecek hesap bulunamadı.")
                {
                    return Unauthorized("Güncellenecek hesap bulunamadı.");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}
