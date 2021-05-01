using System;
using System.Threading.Tasks;
using Domain.RefreshTokens;

namespace Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MemberContext _memberContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenRepository" /> class.
        /// </summary>
        /// <param name="memberContext">Member Db Context.</param>
        public RefreshTokenRepository(MemberContext memberContext) => _memberContext = memberContext ?? throw new ArgumentNullException(nameof(memberContext));

        /// <inheritdoc/>
        public async Task Add(RefreshToken refreshToken) => await _memberContext.RefreshTokens.AddAsync(entity: refreshToken).ConfigureAwait(false);
    }
}