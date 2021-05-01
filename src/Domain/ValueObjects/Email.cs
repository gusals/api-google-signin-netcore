using System;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Email Value-Object.
    /// </summary>
    public readonly struct Email : IEquatable<Email>
    {
        /// <inheritdoc/>
        public string Value { get; }

        /// <inheritdoc/>
        public Email(string value) => Value = value;

        /// <inheritdoc/>
        public bool Equals(Email other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Email email && Equals(other: email);

        /// <inheritdoc/>
        public static bool operator ==(Email left, Email right) => left.Equals(other: right);

        /// <inheritdoc/>
        public static bool operator !=(Email left, Email right) => !(left == right);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(value1: Value);
    }
}