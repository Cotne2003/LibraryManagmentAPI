using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagmentAPI.Services
{
    public class AuthorsService : IAuthorsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Author> AddAuthor([FromBody] AddAuthorDto addAuthorDto)
        {
            var mappedAuthor = _mapper.Map<Author>(addAuthorDto);

            await _context.Authors.AddAsync(mappedAuthor);
            await _context.SaveChangesAsync();

            return mappedAuthor;
        }

        public async Task DeleteAuthor(Guid id)
        {
            var deleteAuthor = await _context.Authors.FindAsync(id);
            if (deleteAuthor is null)
                throw new KeyNotFoundException($"Author with ID {id} not found");

            _context.Authors.Remove(deleteAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();

            var authorDtos = _mapper.Map<List<AuthorDto>>(authors);
            return authorDtos;
        }
    }
}
