using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsService;

        public AuthorsController(IAuthorsRepository authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authorDtos = await _authorsService.GetAllAuthors();

            return Ok(authorDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorDto addAuthorDto)
        {
            var mappedAuthor = await _authorsService.AddAuthor(addAuthorDto);

            return CreatedAtAction(nameof(GetAllAuthors), new { id = mappedAuthor.Id }, mappedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _authorsService.DeleteAuthor(id);

            return NoContent();
        }
    }
}
