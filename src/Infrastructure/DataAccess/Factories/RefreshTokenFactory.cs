using System;
using Domain.RefreshTokens;
using Domain.ValueObjects;

namespace Infrastructure.DataAccess.Factories
{
    /// <inheritdoc/>
    public sealed class RefreshTokenFactory : IRefreshTokenFactory
    {
        /// <inheritdoc/>
        public RefreshToken Create(
            RefreshTokenId id,
            MemberId memberId,
            string value,
            DateTime expiresAt,
            IpAddress createdByIp,
            DateTime createdAt) =>
                new(
                    id: id,
                    memberId: memberId,
                    value: value,
                    expiresAt: expiresAt,
                    createdByIp: createdByIp,
                    createdAt: createdAt);
    }
}