using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.UseCases.OAuthSignin;
using Domain.Enumerations;
using Domain.Members;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Modules.Flags;
using WebApi.ViewModels;

namespace WebApi.UseCases.V1.Logins
{
    /// <summary>
    /// Logins Controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public sealed class LoginsController : ControllerBase, IOutputPort
    {
        private IActionResult? _viewModel;

        void IOutputPort.InvalidToken() => _viewModel = BadRequest();
        void IOutputPort.Ok(Token token, Member member)
        {
            Response.Cookies.Append(key: CookieName.ACCESS_TOKEN, value: token.AccessToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append(key: CookieName.REFRESH_TOKEN, value: token.RefreshToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            _viewModel = Ok(value: new LoginResponse(member: new MemberModel(username: member.Username, name: member.FullName, profileUri: member.ProfileUri)));
        }
        void IOutputPort.Created(Token token, Member member)
        {
            Response.Cookies.Append(key: CookieName.ACCESS_TOKEN, value: token.AccessToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append(key: CookieName.REFRESH_TOKEN, value: token.RefreshToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            _viewModel = Created(
                uri: Url.Link(routeName: RouteName.GET_MEMBER, values: new { id = member.Id }),
                value: new LoginResponse(member: new MemberModel(username: member.Username, name: member.FullName, profileUri: member.ProfileUri)));
        }

        /// <summary>
        /// Google OAuth Login.
        /// </summary>
        /// <remarks>
        /// 구글 OAuth 로그인 후, 회원 가입과 Token 정보를 가져온다.
        /// </remarks>
        /// <response code="201">Login Member.</response>
        /// <response code="201">New Member.</response>
        /// <response code="400">Bad Request.</response>
        /// <param name="useCase">Use Case.</param>
        /// <param name="token">Google IdToken.</param>
        /// <returns><see cref="LoginResponse"/></returns>
        [HttpPost("Google", Name = RouteName.GOOGLE_LOGIN)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromServices] IOAuthSigninUseCase useCase,
            [FromBody][Required] string token)
        {
            useCase.SetOutputPort(outputPort: this);
            await useCase.Execute(provider: ProviderTypes.GOOGLE, idToken: token).ConfigureAwait(continueOnCapturedContext: false);
            return _viewModel!;
        }
    }
}