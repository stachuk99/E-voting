using EVoting_backend.API.Response;
using EVoting_backend.DB.Models;
using System.Threading.Tasks;

namespace EVoting_backend.Services
{
    public class Authenticator
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly TokenManager _tokenRepository;

        public Authenticator(TokenGenerator tokenGenerator, TokenManager tokenRepository)
        {
            _tokenGenerator = tokenGenerator;
            _tokenRepository = tokenRepository;
        }

        public Task<AuthenticatedResponse> Authenticate(User user)
        {
            string accessToken = _tokenGenerator.GenerateAccessToken(user);
            return Task.FromResult(new AuthenticatedResponse
            {
                AccessToken = accessToken
            });
        }


    }
}
