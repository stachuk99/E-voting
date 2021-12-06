using System;
using System.ComponentModel.DataAnnotations;

namespace EVoting_backend.DB.Models
{
    public class Form
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
    }
}
