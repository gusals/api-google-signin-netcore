using System;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Token Value-Object.
    /// </summary>
    public readonly struct Token : IEquatable<Token>
    {
        /// <inheritdoc/>
        public string AccessToken { get; }

        /// <inheritdoc/>
        public string RefreshToken { get; }

        /// <inheritdoc/>
        public double IssuedIn { get; }

        /// <inheritdoc/>
        public double ExpiresIn { get; }

        /// <inheritdoc/>
        public Token(string accessToken, string refreshToken, double issuedIn, double expiresIn) =>
            (AccessToken, RefreshToken, IssuedIn, ExpiresIn) = (accessToken, refreshToken, issuedIn, expiresIn);

        /// <inheritdoc/>
        public bool Equals(Token other) =>
            AccessToken == other.AccessToken && RefreshToken == other.RefreshToken && IssuedIn == other.IssuedIn && ExpiresIn == other.ExpiresIn;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Token token && Equals(other: token);

        /// <inheritdoc/>
        public static bool operator ==(Token left, Token right) => left.Equals(other: right);

        /// <inheritdoc/>
        public static bool operator !=(Token left, Token right) => !(left == right);

        /// <inheritdoc/>
        public override string ToString() => string.Format($"{AccessToken} {RefreshToken} {IssuedIn} {ExpiresIn}");

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(value1: AccessToken, value2: RefreshToken, value3: IssuedIn, value4: ExpiresIn);
    }
}