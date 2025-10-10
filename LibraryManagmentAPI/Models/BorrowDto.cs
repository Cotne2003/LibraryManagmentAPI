using LibraryManagmentAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Models
{
    public class BorrowDto
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public Guid MemberId { get; set; }

        public DateTime BorrowTime { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
