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

namespace WebApi.UseCases.V1.Authentications
{
    /// <summary>
    /// Authentication Controller.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationsController : ControllerBase, IOutputPort
    {
        private IActionResult? _viewModel;

        void IOutputPort.InvalidToken() => _viewModel = BadRequest();
        void IOutputPort.Ok(Token token, Member member)
        {
            Response.Cookies.Append(key: CookieName.ACCESS_TOKEN, value: token.AccessToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append(key: CookieName.REFRESH_TOKEN, value: token.RefreshToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            _viewModel = Ok(value: new AuthenticationResponse(profile: new ProfileModel(id: member.Id, username: member.Username, name: member.FullName, profileUri: member.ProfileUri)));
        }
        void IOutputPort.Created(Token token, Member member)
        {
            Response.Cookies.Append(key: CookieName.ACCESS_TOKEN, value: token.AccessToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append(key: CookieName.REFRESH_TOKEN, value: token.RefreshToken, options: new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            _viewModel = Created(
                uri: Url.Link(routeName: RouteName.GET_USER, values: new { id = member.Id.ToString() }),
                value: new AuthenticationResponse(profile: new ProfileModel(id: member.Id, username: member.Username, name: member.FullName, profileUri: member.ProfileUri)));
        }

        /// <summary>
        /// Google OAuth Login.
        /// </summary>
        /// <remarks>
        /// 구글 OAuth 로그인 후, 회원 가입과 Token 정보를 가져온다.
        /// </remarks>
        /// <response code="200">Existing User.</response>
        /// <response code="201">New User.</response>
        /// <response code="400">Bad Request.</response>
        /// <param name="useCase">Use Case.</param>
        /// <param name="token">Google IdToken.</param>
        /// <returns><see cref="AuthenticationResponse"/></returns>
        [HttpPost("google", Name = RouteName.AUTHENTICATE_GOOGLE)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthenticationResponse))]
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