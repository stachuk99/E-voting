namespace EVoting_backend.API.Request
{
    public class PostVoteRequest
    {
        public int FormId { get; set; }
        public string VoteData { get; set; }
    }
}
