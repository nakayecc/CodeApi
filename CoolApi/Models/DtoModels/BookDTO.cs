using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolApi.Models.DtoModels
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public int AuthorId { get; set; }

    }
}