using System;
using Domain.ValueObjects;

namespace Domain.RefreshTokens
{
    /// <summary>
    /// RefreshToken Null-Object.
    /// </summary>
    public sealed class NullRefreshToken : IRefreshToken
    {
        /// <summary>
        /// Null Instance.
        /// </summary>
        public static NullRefreshToken Instance { get; } = new();

        /// <inheritdoc/>
        public RefreshTokenId Id => new(value: Guid.Empty);
    }
}