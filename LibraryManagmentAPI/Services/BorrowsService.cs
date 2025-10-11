using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Services
{
    public class BorrowsService : IBorrowsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BorrowsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BorrowDto> AddBorrow([FromBody] AddBorrowDto addBorrowDto)
        {
            var mappedBorrow = _mapper.Map<Borrow>(addBorrowDto);

            var borrowedBook = await _context.Books.FindAsync(addBorrowDto.BookId);

            if (borrowedBook is null)
                throw new KeyNotFoundException($"No book found with ID: {addBorrowDto.BookId}");
            else if (borrowedBook.IsAvailable == false)
                throw new InvalidOperationException($"{borrowedBook.Title} is not available right now.");

            borrowedBook.IsAvailable = false;
            _context.Books.Update(borrowedBook);

            var borrowDto = _mapper.Map<BorrowDto>(mappedBorrow);

            await _context.Borrows.AddAsync(mappedBorrow);
            await _context.SaveChangesAsync();

            return borrowDto;
        }

        public async Task DeleteBorrow(Guid id)
        {
            var borrow = await _context.Borrows.FindAsync(id);

            if (borrow is null)
                throw new KeyNotFoundException($"No borrow record found with ID: {id}");

            var borrowedBook = await _context.Books.FindAsync(borrow.BookId);
            if (borrowedBook is null)
                throw new KeyNotFoundException($"No book found with ID: {borrow.BookId}");

            borrowedBook.IsAvailable = true;
            borrow.ReturnDate = DateTime.UtcNow;

            _context.Books.Update(borrowedBook);
            _context.Borrows.Remove(borrow);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BorrowDto>> GetAllBorrows()
        {
            var borrows = await _context.Borrows.ToListAsync();
            var mappedBorrowDtos = _mapper.Map<List<BorrowDto>>(borrows);

            return mappedBorrowDtos;
        }
    }
}
