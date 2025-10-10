using LibraryManagmentAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required List<Guid> BookIds { get; set; }
    }
}
