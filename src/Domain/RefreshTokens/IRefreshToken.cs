using Domain.ValueObjects;

namespace Domain.RefreshTokens
{
    /// <summary>
    /// RefreshToken Interface.
    /// </summary>
    public interface IRefreshToken
    {
        /// <summary>
        /// Get Id.
        /// </summary>
        RefreshTokenId Id { get; }
    }
}