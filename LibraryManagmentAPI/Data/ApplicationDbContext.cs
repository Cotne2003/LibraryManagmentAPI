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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Book)
                .WithMany(b => b.Borrows)
                .HasForeignKey(b => b.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Member)
                .WithMany(m => m.Borrows)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
