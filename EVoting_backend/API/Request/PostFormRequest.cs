using EVoting_backend.DB.Models;
using System;
using System.Collections.Generic;

namespace EVoting_backend.API.Request
{
    public class PostFormRequest
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<SubFormRequest> SubForms { get; set; }
    }

    public class SubFormRequest
    {
        public SubFormType Type { get; set; }
        public List<FormOptionRequest> Options { get; set; }
    }

    public class FormOptionRequest
    {
        public int Ident { get; set; }
        public string Name { get; set; }
    }
}
