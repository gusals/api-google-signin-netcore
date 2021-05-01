using Domain.Enumerations;
using Domain.Members;
using Domain.ValueObjects;

namespace Infrastructure.DataAccess.Factories
{
    /// <inheritdoc/>
    public sealed class MemberFactory : IMemberFactory
    {
        /// <inheritdoc/>
        public Member Create(
            MemberId id,
            ProviderTypes provider,
            Email username,
            string fullName,
            string surname,
            string givenName,
            string profileUri) =>
                new(
                    id: id,
                    provider: provider,
                    username: username,
                    fullName: fullName,
                    surname: surname,
                    givenName: givenName,
                    profileUri: profileUri);
    }
}