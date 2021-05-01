using System.Threading.Tasks;
using Domain.Enumerations;
using Domain.ValueObjects;

namespace Domain.Members
{
    /// <summary>
    /// Member Repository.
    /// </summary>
    public interface IMemberRepository
    {
        /// <summary>
        /// 회원 추가.
        /// </summary>
        /// <param name="member">추가할 회원.</param>
        /// <returns><see cref="Task"/></returns>
        Task Add(Member member);

        /// <summary>
        /// 회원 조회.
        /// </summary>
        /// <param name="provider">공급자.</param>
        /// <param name="username">사용자 이름.</param>
        /// <returns><see cref="IMember"/></returns>
        Task<IMember> Get(ProviderTypes provider, Email username);
    }
}