namespace Domain.Profiles
{
    /// <summary>
    /// Profile Entity.
    /// </summary>
    public sealed class Profile : IProfile
    {
        /// <summary>
        /// Profile Constructor.
        /// </summary>
        /// <param name="username">사용자 이름.</param>
        /// <param name="fullName">풀네임.</param>
        /// <param name="surname">성.</param>
        /// <param name="givenName">명.</param>
        /// <param name="profileUri">프로필 주소.</param>
        public Profile(
            string username,
            string fullName,
            string surname,
            string givenName,
            string profileUri)
        {
            Username = username;
            FullName = fullName;
            Surname = surname;
            GivenName = givenName;
            ProfileUri = profileUri;
        }

        /// <summary>
        /// 사용자 이름.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// 풀네임.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// 성.
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// 명.
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// 프로필 주소.
        /// </summary>
        public string ProfileUri { get; }
    }
}