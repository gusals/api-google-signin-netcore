using System;
using System.Threading.Tasks;
using Application.Services;
using Domain.Enumerations;
using Domain.Members;
using Domain.Profiles;
using Domain.RefreshTokens;
using Domain.ValueObjects;
using Gusals.Extensions;

namespace Application.UseCases.OAuthSignin
{
    /// <inheritdoc/>
    public sealed class OAuthSigninUseCase : IOAuthSigninUseCase
    {
        private readonly IProfileService _profileService;
        private readonly ITokenService _tokenService;
        private readonly IMemberFactory _memberFactory;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly IMemberRepository _memberRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthSigninUseCase" /> class.
        /// </summary>
        /// <param name="profileService">Profile Service.</param>
        /// <param name="tokenService">Token Service.</param>
        /// <param name="memberFactory">Member Factory.</param>
        /// <param name="refreshTokenFactory">RefreshToken Factory.</param>
        /// <param name="memberRepository">Member Repository.</param>
        /// <param name="refreshTokenRepository">RefreshToken Repository.</param>
        /// <param name="unitOfWork">Unit Of Work.</param>
        public OAuthSigninUseCase(
            IProfileService profileService,
            ITokenService tokenService,
            IMemberFactory memberFactory,
            IRefreshTokenFactory refreshTokenFactory,
            IMemberRepository memberRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork)
        {
            _profileService = profileService;
            _tokenService = tokenService;
            _memberFactory = memberFactory;
            _refreshTokenFactory = refreshTokenFactory;
            _memberRepository = memberRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _outputPort = new OAuthSigninPresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

        /// <inheritdoc />
        public Task Execute(ProviderTypes provider, string idToken) => OAuthSignin(provider: provider, idToken: idToken);

        private async Task OAuthSignin(ProviderTypes provider, string idToken)
        {
            var profilePayload = provider switch
            {
                ProviderTypes.GOOGLE => await _profileService.GetGoogleProfile(idToken).ConfigureAwait(continueOnCapturedContext: false),
                _ => throw new NotImplementedException(),
            };

            if (profilePayload is Profile profile)
            {
                var username = new Email(value: profile.Username);
                var token = _tokenService.GenerateToken(username: profile.Username, name: profile.FullName);
                var memberEntity = await _memberRepository.Get(provider: provider, username: username).ConfigureAwait(continueOnCapturedContext: false);

                if (memberEntity is Member member)
                {
                    _outputPort.Ok(token: token, member: member);
                    return;
                }

                var newMemberEntity = _memberFactory.Create(
                    id: new(value: Guid.NewGuid()),
                    provider: provider,
                    username: username,
                    fullName: profile.FullName,
                    surname: profile.Surname,
                    givenName: profile.GivenName,
                    profileUri: profile.ProfileUri);
                await _memberRepository.Add(member: newMemberEntity).ConfigureAwait(continueOnCapturedContext: false);

                var newRefreshTokenEntity = _refreshTokenFactory.Create(
                    id: new(value: Guid.NewGuid()),
                    memberId: newMemberEntity.Id,
                    value: token.RefreshToken,
                    expiresAt: token.ExpiresIn.ToDateTime(),
                    createdByIp: new IpAddress(),
                    createdAt: token.IssuedIn.ToDateTime());
                await _refreshTokenRepository.Add(refreshToken: newRefreshTokenEntity).ConfigureAwait(continueOnCapturedContext: false);

                await _unitOfWork.Save().ConfigureAwait(continueOnCapturedContext: false);

                _outputPort.Created(token: token, member: newMemberEntity);
                return;
            }

            _outputPort.InvalidToken();
        }
    }
}