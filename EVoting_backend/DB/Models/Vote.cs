using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVoting_backend.DB.Models
{
    public class Vote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "text")]
        public string Data { get; set; }
        public int FormId { get; set; }
        public Form Form { get; set; }

    }
}
