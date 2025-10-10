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
    public class BorrowsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BorrowsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrows()
        {
            var borrows = await _dbContext.Borrows.ToListAsync();
            var mappedBorrowDtos = _mapper.Map<List<BorrowDto>>(borrows);

            return Ok(mappedBorrowDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBorrow(AddBorrowDto addBorrowDto)
        {
            var mappedBorrow = _mapper.Map<Borrow>(addBorrowDto);

            var borrowedBook = await _dbContext.Books.FindAsync(addBorrowDto.BookId);

            if (borrowedBook is null)
                return NotFound($"No book found with ID: {addBorrowDto.BookId}");
            else if (borrowedBook.IsAvailable == false)
                return BadRequest($"{borrowedBook.Title} is not available right now.");

            borrowedBook.IsAvailable = false;
            _dbContext.Books.Update(borrowedBook);

            var borrowDto = _mapper.Map<BorrowDto>(mappedBorrow);

            await _dbContext.Borrows.AddAsync(mappedBorrow);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllBorrows), new { id = mappedBorrow.Id }, borrowDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ReturnBorrow(Guid id)
        {
            var borrow = await _dbContext.Borrows.FindAsync(id);

            if (borrow is null)
                return NotFound($"No borrow record found with ID: {id}");

            var borrowedBook = await _dbContext.Books.FindAsync(borrow.BookId);
            if (borrowedBook is null)
                return NotFound($"No book found with ID: {borrow.BookId}");

            borrowedBook.IsAvailable = true;
            borrow.ReturnDate = DateTime.UtcNow;

            _dbContext.Books.Update(borrowedBook);
            _dbContext.Borrows.Remove(borrow);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
