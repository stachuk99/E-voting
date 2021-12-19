using System.Text.Json.Serialization;

namespace EVoting_backend.API.Response
{
    public class AuthenticatedResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }
    }
}
