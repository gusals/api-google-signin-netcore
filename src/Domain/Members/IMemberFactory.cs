using System;
using Domain.Enumerations;
using Domain.ValueObjects;

namespace Domain.Members
{
    /// <summary>
    /// Member Entity Factory.
    /// </summary>
    public interface IMemberFactory
    {
        /// <summary>
        /// Creates a new Member.
        /// </summary>
        /// <param name="id">회원 고유 식별자.</param>
        /// <param name="provider">공급자.</param>
        /// <param name="username">사용자 이름.</param>
        /// <param name="fullName">풀네임.</param>
        /// <param name="surname">성.</param>
        /// <param name="givenName">명.</param>
        /// <param name="profileUri">프로필 주소.</param>
        /// <param name="registeredAt">가입 일시.</param>
        /// <returns><see cref="Member"/></returns>
        Member Create(
            MemberId id,
            ProviderTypes provider,
            Email username,
            string fullName,
            string surname,
            string givenName,
            string profileUri,
            DateTime registeredAt);
    }
}