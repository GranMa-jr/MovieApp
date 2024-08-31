using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entity.Dtos.WatchHistoryDtos;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstract;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchHistoryController : ControllerBase
    {
        private readonly IWatchHistoryService _watchHistoryService;

        public WatchHistoryController(IWatchHistoryService watchHistoryService)
        {
            _watchHistoryService = watchHistoryService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create([FromBody] WatchHistoryAddDto watchHistoryAddDto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                await _watchHistoryService.CreateAsync(watchHistoryAddDto, appUserId);

                return Created();
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

        [HttpGet("get-all-of-film-by-user/{userId}")]
        public async Task<IActionResult> GetAllUserWatchHistories(int userId)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                var all = await _watchHistoryService.GetAllByUserAsync(appUserId);

                return Ok(all);
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
		[HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }
                await _watchHistoryService.DeleteAsync(Id, appUserId); //

                return Ok(Id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Kullanıcı bulunamadı.")
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }
                else if (ex.Message == "Bu işlemi yapmak için yetkiniz yok.")
                {
                    return Unauthorized("Bu işlemi yapmak için yetkiniz yok.");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}
