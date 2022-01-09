using System;
using System.Collections.Generic;

namespace EVoting_backend.API.Response
{
    public class FormDefinitionsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<SubFormResponse> SubForms { get; set; }
    }

    public class SubFormResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChoicesLimit { get; set; }
        public List<FormOptionResponse> Options { get; set; }
    }

    public class FormOptionResponse
    {
        public int Ident { get; set; }
        public string Name { get; set; }
    }
}
