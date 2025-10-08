namespace LibraryManagmentAPI.Models.Entities
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        public ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
