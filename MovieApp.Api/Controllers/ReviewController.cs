﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entity.Dtos.ReviewDtos;
using MovieApp.Entity.Entities;
using MovieApp.Service.Services.Abstract;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReviewAddDto reviewAddDto)
        {
            try
            {
                var appUserIdString = (string)HttpContext.Items["unique_name"];

                if (!int.TryParse(appUserIdString, out int appUserId))
                {
                    return Unauthorized("Geçersiz kullanıcı kimliği.");
                }

                await _reviewService.CreateAsync(reviewAddDto, appUserId);

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
    }
}