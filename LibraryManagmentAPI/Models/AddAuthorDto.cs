using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class AddAuthorDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }
    }
}
