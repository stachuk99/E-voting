using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVoting_backend.DB.Models
{
    public class FormOption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public int Ident { get; set; }
        public string Name { get; set; }
        public int SubFormId { get; set; }
        public long Count { get; set; }
        public SubForm SubForm { get; set; }
    }
}
