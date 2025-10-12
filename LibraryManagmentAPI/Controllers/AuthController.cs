using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.TestUser;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
    }
}
