using Domain.Members;
using Domain.ValueObjects;

namespace Application.UseCases.OAuthSignin
{
    /// <summary>
    /// Output Port.
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        /// InvalidToken.
        /// </summary>
        void InvalidToken();

        /// <summary>
        /// User Signin.
        /// </summary>
        /// <param name="token">토큰.</param>
        /// <param name="member">로그인한 사용자.</param>
        void Ok(Token token, Member member);

        /// <summary>
        /// User Registered.
        /// </summary>
        /// <param name="token">토큰.</param>
        /// <param name="member">신규 가입한 사용자.</param>
        void Created(Token token, Member member);
    }
}