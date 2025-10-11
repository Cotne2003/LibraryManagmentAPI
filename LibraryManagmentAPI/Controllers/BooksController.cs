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
    public class BooksController : ControllerBase
    {
        public readonly IBooksRepository _booksService;

        public BooksController(IBooksRepository booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var bookDtos = await _booksService.GetAllBooks();

            return Ok(bookDtos);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAllAvailableBooks()
        {
            var availableBookDtos = await _booksService.GetAllAvailableBooks();

            return Ok(availableBookDtos);
        }

        [HttpGet("unavailable")]
        public async Task<IActionResult> GetUnavailableBooks()
        {
            var unavailableBookDtos = await _booksService.GetUnavailableBooks();

            return Ok(unavailableBookDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            var bookDto = await _booksService.AddBook(addBookDto);

            return CreatedAtAction(nameof(GetAllBooks), new { id = bookDto.Id }, bookDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _booksService.DeleteBook(id);

            return NoContent();
        }
    }
}
