using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models.Entities
{
    public class Borrow
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BookId { get; set; }
        [Required]
        public Book Book { get; set; } = null!;

        [Required]
        public Guid MemberId { get; set; }
        [Required]
        public Member Member { get; set; } = null!;

        public DateTime BorrowTime {  get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate {  get; set; }
    }
}
