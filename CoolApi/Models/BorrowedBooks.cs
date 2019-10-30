using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoolApi.Model;

namespace CoolApi.Models
{
    [Table("BorrowedBooks")]
    public class BorrowedBooks
    {
        public int Id { get; set; }
        
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User User { get; set; }
    }
}
