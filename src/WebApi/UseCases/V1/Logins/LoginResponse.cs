using System.ComponentModel.DataAnnotations;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Logins
{
    /// <summary>
    /// The response for Login.
    /// </summary>
    public sealed class LoginResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse" /> class.
        /// </summary>
        /// <param name="member">회원.</param>
        public LoginResponse(MemberModel member) => Member = member;

        /// <summary>
        /// 회원.
        /// </summary>
        [Required] public MemberModel Member { get; }
    }
}