using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Unit Of Work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 데이터베이스 변경 사항을 적용.
        /// </summary>
        /// <returns><see cref="int"/></returns>
        Task<int> Save();
    }
}