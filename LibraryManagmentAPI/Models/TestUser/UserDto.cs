using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models.TestUser
{
    public class UserDto
    {
        [Required]
        public required string Name { get; set; } = string.Empty;
        [Required]
        public required string Password { get; set; } = string.Empty;
    }
}
