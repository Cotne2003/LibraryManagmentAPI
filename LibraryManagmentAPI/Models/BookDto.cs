using LibraryManagmentAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;

        public required List<Guid> AuthorIds { get; set; }
    }
}
