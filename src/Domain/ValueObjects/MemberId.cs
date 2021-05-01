using System;
using Gusals.Extensions;

namespace Domain.ValueObjects
{
    /// <summary>
    /// MemberId Value-Object.
    /// </summary>
    public readonly struct MemberId : IEquatable<MemberId>
    {
        /// <inheritdoc/>
        public long Value { get; }

        /// <inheritdoc/>
        public MemberId(Guid value) => Value = value.ToLong();

        /// <inheritdoc/>
        public MemberId(long value) => Value = value;

        /// <inheritdoc/>
        public MemberId(string value) => Value = long.Parse(s: value);

        /// <inheritdoc/>
        public bool Equals(MemberId other) => Value == other.Value;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is MemberId memberId && Equals(other: memberId);

        /// <inheritdoc/>
        public static bool operator ==(MemberId left, MemberId right) => left.Equals(other: right);

        /// <inheritdoc/>
        public static bool operator !=(MemberId left, MemberId right) => !(left == right);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(value1: Value);
    }
}