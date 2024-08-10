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
                await _movieService.CreateAsync(movieAddDto);

                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var all =  await _movieService.GetAllAsync();

                return Ok(all);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] MovieUpdateDto movieUpdateDto)
        {
            try
            {
                await _movieService.UpdateAsync(movieUpdateDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _movieService.DeleteAsync(Id); //

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}