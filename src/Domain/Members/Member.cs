using Domain.Enumerations;
using Domain.ValueObjects;

namespace Domain.Members
{
    /// <summary>
    /// Member Entity.
    /// </summary>
    public sealed class Member : IMember
    {
        /// <summary>
        /// Member Constructor.
        /// </summary>
        /// <param name="id">회원 고유 식별자.</param>
        /// <param name="provider">공급자.</param>
        /// <param name="username">사용자 이름.</param>
        /// <param name="fullName">풀네임.</param>
        /// <param name="surname">성.</param>
        /// <param name="givenName">명.</param>
        /// <param name="profileUri">프로필 주소.</param>
        public Member(
            MemberId id,
            ProviderTypes provider,
            Email username,
            string fullName,
            string surname,
            string givenName,
            string profileUri)
        {
            Id = id;
            Provider = provider;
            Username = username;
            FullName = fullName;
            Surname = surname;
            GivenName = givenName;
            ProfileUri = profileUri;
        }

        /// <summary>
        /// 회원 고유 식별자.
        /// </summary>
        public MemberId Id { get; }

        /// <summary>
        /// 공급자.
        /// </summary>
        public ProviderTypes Provider { get; }

        /// <summary>
        /// 사용자 이름.
        /// </summary>
        public Email Username { get; }

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