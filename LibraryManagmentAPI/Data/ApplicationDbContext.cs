using LibraryManagmentAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Borrow> Borrows { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
    }
}
