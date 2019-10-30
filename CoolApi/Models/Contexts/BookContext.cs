﻿using CoolApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CoolApi.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<BorrowedBooks> BorrowedBookses { get; set; }


    }
}