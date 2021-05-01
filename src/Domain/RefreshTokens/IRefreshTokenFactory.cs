using System;
using Domain.ValueObjects;

namespace Domain.RefreshTokens
{
    /// <summary>
    /// RefreshToken Entity Factory.
    /// </summary>
    public interface IRefreshTokenFactory
    {
        /// <summary>
        /// Creates a new RefreshToken.
        /// </summary>
        /// <param name="id">리프레쉬 토큰 고유 식별자.</param>
        /// <param name="memberId">회원 고유 식별자.</param>
        /// <param name="value">토큰.</param>
        /// <param name="expiresAt">만료 일시.</param>
        /// <param name="createdByIp">생성한 아이피.</param>
        /// <param name="createdAt">생성한 일시.</param>
        /// <returns><see cref="RefreshToken"/></returns>
        RefreshToken Create(
            RefreshTokenId id,
            MemberId memberId,
            string value,
            DateTime expiresAt,
            IpAddress createdByIp,
            DateTime createdAt);
    }
}