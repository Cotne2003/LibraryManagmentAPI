using LibraryManagmentAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class AddBookDto
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public required string Title { get; set; }
        [Required]
        [StringLength(20)]
        public required string ISBN { get; set; }
        [Required]
        [Range(1000, 2100)]
        public required int Year { get; set; }

        [Required]
        public required List<Guid> AuthorIds { get; set; }
    }
}
