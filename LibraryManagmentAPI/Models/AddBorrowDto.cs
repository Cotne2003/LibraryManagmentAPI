using LibraryManagmentAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class AddBorrowDto
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        public DateTime BorrowTime { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
    }
}
