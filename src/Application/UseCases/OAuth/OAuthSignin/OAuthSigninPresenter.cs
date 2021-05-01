using Domain.Members;
using Domain.ValueObjects;

namespace Application.UseCases.OAuthSignin
{
    /// <inheritdoc/>
    public sealed class OAuthSigninPresenter : IOutputPort
    {
        /// <inheritdoc/>
        public bool? InvalidTokenOutput { get; private set; }

        /// <inheritdoc/>
        public void InvalidToken() => InvalidTokenOutput = true;

        /// <inheritdoc/>
        public Token? Token { get; private set; }

        /// <inheritdoc/>
        public Member? Member { get; private set; }

        /// <inheritdoc/>
        public void Ok(Token token, Member member) => (Token, Member) = (token, member);

        /// <inheritdoc/>
        public void Created(Token token, Member member) => (Token, Member) = (token, member);
    }
}