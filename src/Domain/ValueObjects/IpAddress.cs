using System;

namespace Domain.ValueObjects
{
    /// <summary>
    /// IpAddress Value-Object.
    /// </summary>
    public readonly struct IpAddress : IEquatable<IpAddress>
    {
        /// <inheritdoc/>
        public string Value { get; }

        /// <inheritdoc/>
        public IpAddress(string value) => Value = value;

        /// <inheritdoc/>
        public bool Equals(IpAddress other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is IpAddress ipAddress && Equals(other: ipAddress);

        /// <inheritdoc/>
        public static bool operator ==(IpAddress left, IpAddress right) => left.Equals(other: right);

        /// <inheritdoc/>
        public static bool operator !=(IpAddress left, IpAddress right) => !(left == right);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(value1: Value);
    }
}