using System.ComponentModel.DataAnnotations;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Authentications
{
    /// <summary>
    /// The response for Authentication.
    /// </summary>
    public sealed class AuthenticationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationResponse" /> class.
        /// </summary>
        /// <param name="profile">사용자 프로필.</param>
        public AuthenticationResponse(ProfileModel profile) => Profile = profile;

        /// <summary>
        /// 사용자 프로필.
        /// </summary>
        [Required] public ProfileModel Profile { get; }
    }
}