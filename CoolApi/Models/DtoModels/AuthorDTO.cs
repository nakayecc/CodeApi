using System.Collections.Generic;
using CoolApi.Model;

namespace CoolApi.Models.DtoModels
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SingleBookDTO> Books { get; set; }
    }
}