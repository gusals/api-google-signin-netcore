using Domain.ValueObjects;

namespace Domain.Members
{
    /// <summary>
    /// Member Interface.
    /// </summary>
    public interface IMember
    {
        /// <summary>
        /// Get Id.
        /// </summary>
        MemberId Id { get; }
    }
}