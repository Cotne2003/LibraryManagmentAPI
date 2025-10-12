using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models.TestUser
{
    public class User
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
