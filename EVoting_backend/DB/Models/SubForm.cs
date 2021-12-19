using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVoting_backend.DB.Models
{
    public class SubForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public SubFormType Type { get; set; }
        public int FormId { get; set; }
        public Form Form { get; set; }
        public ICollection<FormOption> Options { get; set; }
    }
    public enum SubFormType
    {
        SingleChoice = 0,
        MultipleChoice = 1
    }
}
