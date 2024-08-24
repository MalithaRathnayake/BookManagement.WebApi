using BookManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Configurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Author).IsRequired().HasMaxLength(100);
            builder.Property(e => e.PublishedDate).IsRequired();
        }
    }
}
