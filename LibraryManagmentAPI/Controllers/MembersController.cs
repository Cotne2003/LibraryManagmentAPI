using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersRepository _membersService;
        public MembersController(IMembersRepository memberRepository)
        {
            _membersService = memberRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GettAllMembers()
        {
            var members = await _membersService.GetAllMembers();

            return Ok(members);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] AddMemberDto addMemberDto)
        {
            var mappedMember = await _membersService.AddMember(addMemberDto);

            return CreatedAtAction(nameof(GettAllMembers), new { id = mappedMember.Id, mappedMember });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            await _membersService.DeleteMember(id);

            return NoContent();
        }
    }
}
