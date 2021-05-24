using System;
using Domain.ValueObjects;

namespace Domain.RefreshTokens
{
    /// <summary>
    /// RefreshToken Entity.
    /// </summary>
    public sealed class RefreshToken : IRefreshToken
    {
        /// <summary>
        /// RefreshToken Constructor.
        /// </summary>
        /// <param name="id">리프레쉬 토큰 고유 식별자.</param>
        /// <param name="memberId">회원 고유 식별자.</param>
        /// <param name="value">토큰.</param>
        /// <param name="expiresAt">만료 일시.</param>
        /// <param name="createdByIp">생성한 아이피.</param>
        /// <param name="createdAt">생성한 일시.</param>
        public RefreshToken(
            RefreshTokenId id,
            MemberId memberId,
            string value,
            DateTime expiresAt,
            string createdByIp,
            DateTime createdAt)
        {
            Id = id;
            MemberId = memberId;
            Value = value;
            ExpiresAt = expiresAt;
            CreatedByIp = createdByIp;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// 리프레쉬 토큰 고유 식별자.
        /// </summary>
        public RefreshTokenId Id { get; }

        /// <summary>
        /// 토큰.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 만료 일시.
        /// </summary>
        public DateTime ExpiresAt { get; }

        /// <summary>
        /// 생성한 아이피.
        /// </summary>
        public string CreatedByIp { get; }

        /// <summary>
        /// 생성한 일시.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// 취소한 아이피.
        /// </summary>
        public string? RevokedByIp { get; }

        /// <summary>
        /// 취소한 일시. 
        /// </summary>
        public DateTime? RevokedAt { get; }

        /// <summary>
        /// 회원 고유 식별자.
        /// </summary>
        public MemberId MemberId { get; }
    }
}