using System.Threading.Tasks;
using Domain.Profiles;

namespace Application.Services
{
    /// <summary>
    /// Profile Service.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Get a Google User Profile.
        /// </summary>
        /// <param name="idToken">사용자의 Id Token.</param>
        /// <returns><see cref="IProfile"/></returns>
        Task<IProfile> GetGoogleProfile(string idToken);
    }
}