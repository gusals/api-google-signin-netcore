namespace Domain.Profiles
{
    /// <summary>
    /// Profile Null-Object.
    /// </summary>
    public sealed class NullProfile : IProfile
    {
        /// <summary>
        /// Null Instance.
        /// </summary>
        public static NullProfile Instance { get; } = new();
    }
}