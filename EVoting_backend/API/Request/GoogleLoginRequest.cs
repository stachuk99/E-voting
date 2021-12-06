using System.Text.Json.Serialization;

namespace EVoting_backend.API.Request
{
    public class GoogleLoginRequest
    {
        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }
    }
}
