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
    public class BooksController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly IMapper _mapper;

        public BooksController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _dbContext.Books
                .Include(b => b.Authors)
                .ToListAsync();

            var bookDtos = _mapper.Map<List<BookDto>>(books);

            return Ok(bookDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            var mappedBook = _mapper.Map<Book>(addBookDto);

            var authors = await _dbContext.Authors
                .Where(a => addBookDto.AuthorIds.Contains(a.Id))
                .ToListAsync();

            mappedBook.Authors = authors;

            await _dbContext.Books.AddAsync(mappedBook);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllBooks), new { id = mappedBook.Id }, mappedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var deleteBook = await _dbContext.Books.FindAsync(id);

            if (deleteBook is null)
                return NotFound();

            _dbContext.Books.Remove(deleteBook);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
