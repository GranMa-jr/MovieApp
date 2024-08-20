using Microsoft.AspNetCore.Mvc;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstractions;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MovieAddDto movieAddDto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                await _movieService.CreateAsync(movieAddDto, appUserId);

                return Created();
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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                var all = await _movieService.GetAllAsync(appUserId);

                return Ok(all);
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

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] MovieUpdateDto movieUpdateDto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                await _movieService.UpdateAsync(movieUpdateDto, appUserId);

                return Ok();
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }
                await _movieService.DeleteAsync(Id, appUserId); //

                return Ok();
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