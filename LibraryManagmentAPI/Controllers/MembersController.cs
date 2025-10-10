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
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MembersController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GettAllMembers()
        {
            return Ok(await _dbContext.Members.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] AddMemberDto addMemberDto)
        {
            var mappedMember = _mapper.Map<Member>(addMemberDto);

            await _dbContext.Members.AddAsync(mappedMember);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GettAllMembers), new { id = mappedMember.Id, mappedMember });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            var deleteMember = await _dbContext.Members.FindAsync(id);
            if (deleteMember is null)
                return NotFound();

            _dbContext.Members.Remove(deleteMember);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
