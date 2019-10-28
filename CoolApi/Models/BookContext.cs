using CoolApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoolApi.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}