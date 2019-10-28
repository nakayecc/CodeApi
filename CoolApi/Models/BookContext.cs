using CoolApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoolApi.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}