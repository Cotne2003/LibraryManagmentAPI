using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Repositories
{
    public interface IMembersRepository
    {
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member> AddMember([FromBody] AddMemberDto addMemberDto);
        Task DeleteMember(Guid id);
    }
}
