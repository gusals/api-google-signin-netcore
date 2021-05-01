using System;
using Domain.Enumerations;
using Domain.Members;
using Domain.ValueObjects;
using Gusals.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations
{
    /// <summary>
    /// Member Configuration.
    /// </summary>
    public sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        /// <summary>
        /// Configure Member.
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public void Configure(EntityTypeBuilder<Member> entityTypeBuilder)
        {
            if (entityTypeBuilder == null)
                throw new ArgumentNullException(paramName: nameof(entityTypeBuilder));

            entityTypeBuilder.ToTable("member");

            entityTypeBuilder.Property(member => member.Id)
                .HasConversion(
                    id => id.Value,
                    id => new MemberId(id))
                .IsRequired();

            entityTypeBuilder.Property(member => member.Provider)
                .HasConversion(
                    provider => provider.ToString(),
                    provider => provider.ToEnum<ProviderTypes>())
                .IsRequired();

            entityTypeBuilder.Property(member => member.Username)
                .HasConversion(
                    username => username.Value,
                    username => new Email(username))
                .IsRequired();

            entityTypeBuilder.Property(member => member.FullName)
                .IsRequired();

            entityTypeBuilder.Property(member => member.Surname)
                .IsRequired();

            entityTypeBuilder.Property(member => member.GivenName)
                .IsRequired();

            entityTypeBuilder.Property(member => member.ProfileUri)
                .IsRequired();
        }
    }
}