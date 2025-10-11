using AutoMapper;
using LibraryManagmentAPI.Data;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Services
{
    public class MembersService : IMembersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MembersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Member> AddMember([FromBody] AddMemberDto addMemberDto)
        {
            var mappedMember = _mapper.Map<Member>(addMemberDto);

            await _context.Members.AddAsync(mappedMember);
            await _context.SaveChangesAsync();

            return mappedMember;
        }

        public async Task DeleteMember(Guid id)
        {
            var deleteMember = await _context.Members.FindAsync(id);
            if (deleteMember is null)
                throw new KeyNotFoundException($"Member with ID {id} not found");

            _context.Members.Remove(deleteMember);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _context.Members.ToListAsync();
        }
    }
}
