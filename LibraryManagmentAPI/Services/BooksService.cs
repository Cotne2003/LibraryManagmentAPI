using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Services
{
    public class BooksService : IBooksRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BooksService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDto> AddBook([FromBody] AddBookDto addBookDto)
        {
            var mappedBook = _mapper.Map<Book>(addBookDto);

            var authors = await _context.Authors
                .Where(a => addBookDto.AuthorIds.Contains(a.Id))
                .ToListAsync();

            mappedBook.Authors = authors;

            await _context.Books.AddAsync(mappedBook);
            await _context.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(mappedBook);
            return bookDto;
        }

        public async Task DeleteBook(Guid id)
        {
            var deleteBook = await _context.Books.FindAsync(id);

            if (deleteBook is null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            _context.Books.Remove(deleteBook);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllAvailableBooks()
        {
            var availableBooks = await _context.Books
                .Where(b => b.IsAvailable == true)
                .Include(b => b.Authors)
                .Include(b => b.Borrows)
                .ToListAsync();

            var availableBookDtos = _mapper.Map<List<BookDto>>(availableBooks);
            return availableBookDtos;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Borrows)
                .ToListAsync();

            var bookDtos = _mapper.Map<List<BookDto>>(books);
            return bookDtos;
        }

        public async Task<IEnumerable<BookDto>> GetUnavailableBooks()
        {
            var unavailableBooks = await _context.Books
                .Where(b => b.IsAvailable == false)
                .Include(b => b.Authors)
                .Include(b => b.Borrows)
                .ToListAsync();

            var unavailableBookDtos = _mapper.Map<List<BookDto>>(unavailableBooks);
            return unavailableBookDtos;
        }
    }
}
