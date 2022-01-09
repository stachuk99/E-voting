﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVoting_backend.DB.Models
{
    public class SubForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChoicesLimit { get; set; }
        public int FormId { get; set; }
        public Form Form { get; set; }
        public ICollection<FormOption> Options { get; set; }
    }
}
