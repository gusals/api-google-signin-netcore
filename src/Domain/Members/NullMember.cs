using System;
using Domain.ValueObjects;

namespace Domain.Members
{
    /// <summary>
    /// Member Null-Object.
    /// </summary>
    public sealed class NullMember : IMember
    {
        /// <summary>
        /// Null Instance.
        /// </summary>
        public static NullMember Instance { get; } = new();

        /// <inheritdoc/>
        public MemberId Id => new(value: Guid.Empty);
    }
}