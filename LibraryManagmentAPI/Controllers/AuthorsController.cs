using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _dbContext.Authors
                .Include(a => a.Books)
                .ToListAsync();

            var authorDtos = _mapper.Map<List<AuthorDto>>(authors);

            return Ok(authorDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorDto addAuthorDto)
        {
            var mappedAuthor = _mapper.Map<Author>(addAuthorDto);

            await _dbContext.Authors.AddAsync(mappedAuthor);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllAuthors), new { id = mappedAuthor.Id }, mappedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var deleteAuthor = await _dbContext.Authors.FindAsync(id);
            if (deleteAuthor is null)
                return NotFound();

            _dbContext.Authors.Remove(deleteAuthor);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
