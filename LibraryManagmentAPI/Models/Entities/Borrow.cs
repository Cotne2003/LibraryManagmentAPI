namespace LibraryManagmentAPI.Models.Entities
{
    public class Borrow
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;

        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime BorrowTime {  get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate {  get; set; }
    }
}
