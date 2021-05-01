using System.Threading.Tasks;
using Domain.Enumerations;

namespace Application.UseCases.OAuthSignin
{
    /// <summary>
    /// OAuthSignin Use-Case.
    /// </summary>
    public interface IOAuthSigninUseCase
    {
        /// <summary>
        /// Output Port 작성.
        /// </summary>
        /// <param name="outputPort">출력 포트 메시지.</param>
        void SetOutputPort(IOutputPort outputPort);

        /// <summary>
        /// Use Case 실행.
        /// </summary>
        /// <param name="provider">공급자.</param>
        /// <param name="idToken">사용자의 Id Token.</param>
        /// <returns><see cref="Task"/></returns>
        Task Execute(
            ProviderTypes provider,
            string idToken);
    }
}