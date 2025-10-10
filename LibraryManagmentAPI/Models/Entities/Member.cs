using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models.Entities
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
