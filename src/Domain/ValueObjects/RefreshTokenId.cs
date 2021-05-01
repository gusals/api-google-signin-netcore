using System;
using Gusals.Extensions;

namespace Domain.ValueObjects
{
    /// <summary>
    /// RefreshTokenId Value-Object.
    /// </summary>
    public readonly struct RefreshTokenId : IEquatable<RefreshTokenId>
    {
        /// <inheritdoc/>
        public long Value { get; }

        /// <inheritdoc/>
        public RefreshTokenId(Guid value) => Value = value.ToLong();

        /// <inheritdoc/>
        public RefreshTokenId(long value) => Value = value;

        /// <inheritdoc/>
        public RefreshTokenId(string value) => Value = long.Parse(s: value);

        /// <inheritdoc/>
        public bool Equals(RefreshTokenId other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is RefreshTokenId refreshTokenId && Equals(other: refreshTokenId);

        /// <inheritdoc/>
        public static bool operator ==(RefreshTokenId left, RefreshTokenId right) => left.Equals(other: right);

        /// <inheritdoc/>
        public static bool operator !=(RefreshTokenId left, RefreshTokenId right) => !(left == right);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(value1: Value);
    }
}