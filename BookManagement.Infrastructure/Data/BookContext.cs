using BookManagement.Domain.Models;
using BookManagement.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Data
{
    public class BookContext: DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        }
    }
}
