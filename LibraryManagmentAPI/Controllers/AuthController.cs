using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;

        public AuthController(IAuthRepository authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AddAuthorDto addAuthorDto)
        {
            var author = await _authService.Register(addAuthorDto);

            return Ok(author);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LogAuthorDto logAuthorDto)
        {
            var token = await _authService.Login(logAuthorDto);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var result = await _authService.RefreshTokens(refreshTokenRequestDto);

            return Ok(result);
        }
    }
}
