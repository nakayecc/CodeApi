using System;
using System.ComponentModel.DataAnnotations;

namespace CoolApi.Model
{
    public class Book
    {
        [Display(Name = "ISBN")]
        public string Id { get; set; }
        public string Name { get; set; }
       public DateTime ReleaseDateTime { get; set; }


    }
}