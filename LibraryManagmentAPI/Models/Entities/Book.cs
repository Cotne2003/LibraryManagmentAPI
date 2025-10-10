using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        [Range(1000, 2100)]
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
