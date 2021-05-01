using System;
using System.Threading.Tasks;
using Domain.Enumerations;
using Domain.Members;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc/>
    public sealed class MemberRepository : IMemberRepository
    {
        private readonly MemberContext _memberContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberRepository" /> class.
        /// </summary>
        /// <param name="memberContext">Member Db Context.</param>
        public MemberRepository(MemberContext memberContext) => _memberContext = memberContext ?? throw new ArgumentNullException(nameof(memberContext));

        /// <inheritdoc/>
        public async Task Add(Member member) => await _memberContext.Members.AddAsync(entity: member).ConfigureAwait(false);

        /// <inheritdoc/>
        public async Task<IMember> Get(ProviderTypes provider, Email username)
        {
            var memberEntity =
                await _memberContext.Members
                    .SingleOrDefaultAsync(member => member.Provider.Equals(provider) && member.Username.Equals(username))
                    .ConfigureAwait(false);
            return memberEntity is Member member ? member : NullMember.Instance;
        }
    }
}