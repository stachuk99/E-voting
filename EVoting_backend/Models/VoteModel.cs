using System.Collections.Generic;

namespace EVoting_backend.Models
{
    public class VoteModel
    {
        public int FormId { get; set; }
        public List<SubFormModel> SubForms { get; set; }
    }

    public class SubFormModel
    {
        public int Id { get; set; }
        public List<OptionModel> Options { get; set; }
    }

    public class OptionModel
    {
        public int Ident { get; set; }
        public bool Checked { get; set; }
    }
}
