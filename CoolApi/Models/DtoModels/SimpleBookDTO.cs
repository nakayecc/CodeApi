using System;

namespace CoolApi.Models.DtoModels
{
    public class SimpleBookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDateTime { get; set; }
    }
}