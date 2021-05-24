using System.Threading.Tasks;
using Application.Services;
using Domain.Profiles;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Infrastructure.GoogleApis
{
    /// <inheritdoc/>
    public sealed class GoogleProfileService : IProfileService
    {
        private readonly IConfiguration _configuration;
        private readonly string _client_id;
        private readonly string _client_secret;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleProfileService" /> class.
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public GoogleProfileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client_id = _configuration.GetValue<string>("AuthenticationModule:Google:ClientId");
            _client_secret = _configuration.GetValue<string>("AuthenticationModule:Google:ClientSecret");
        }

        /// <inheritdoc/>
        public async Task<IProfile> GetGoogleProfile(string idToken)
        {
            try
            {
                var payload = await ValidateAsync(idToken, new ValidationSettings { Audience = new[] { _client_id } }).ConfigureAwait(false);
                return new Profile(
                    username: payload.Email,
                    fullName: payload.Name,
                    surname: payload.FamilyName,
                    givenName: payload.GivenName,
                    profileUri: payload.Picture);
            }
            catch
            {
                return NullProfile.Instance;
            }
        }
    }
}