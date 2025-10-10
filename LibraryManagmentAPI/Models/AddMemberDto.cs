using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class AddMemberDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string FullName { get; set; }
    }
}
