using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
