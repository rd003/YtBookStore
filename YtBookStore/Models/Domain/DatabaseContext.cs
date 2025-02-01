using Microsoft.EntityFrameworkCore;

namespace YtBookStore.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                   .HasOne(b => b.Publisher)
                   .WithMany(b => b.Books)
                   .HasForeignKey(a => a.PubhlisherId)
                   .HasConstraintName("FK_Book_Publisher_PublisherId")
                   .IsRequired();

            builder.Entity<Book>()
                   .HasOne(b => b.Author)
                   .WithMany(b => b.Books)
                   .HasForeignKey(b => b.AuthorId)
                   .IsRequired();

            builder.Entity<Book>()
                   .HasOne(b => b.Genre)
                   .WithMany(b => b.Books)
                   .HasForeignKey(b => b.GenreId)
                   .IsRequired();

        }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
