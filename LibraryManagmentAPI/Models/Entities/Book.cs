namespace LibraryManagmentAPI.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
