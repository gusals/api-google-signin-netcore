using System.Threading.Tasks;

namespace Domain.RefreshTokens
{
    /// <summary>
    /// RefreshToken Repository.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// 리프레쉬 토큰 추가.
        /// </summary>
        /// <param name="refreshToken">추가할 리프레쉬 토큰.</param>
        /// <returns><see cref="Task"/></returns>
        Task Add(RefreshToken refreshToken);
    }
}