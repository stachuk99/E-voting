using System;

namespace EVoting_backend.DB.Models
{
    public class UserVoted
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid FormId { get; set; }
        public Form Form { get; set; }
    }
}
