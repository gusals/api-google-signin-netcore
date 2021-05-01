using Domain.ValueObjects;

namespace Application.Services
{
    /// <summary>
    /// Token Service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generate Token.
        /// </summary>
        /// <param name="username">사용자 이름.</param>
        /// <param name="name">이름</param>
        /// <returns><see cref="Token"/></returns>
        Token GenerateToken(string username, string name);
    }
}