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
                .HasColumnName("id")
                .HasColumnType("bigint(20)")
                .HasConversion(
                    id => id.Value,
                    id => new MemberId(id))
                .ValueGeneratedNever()
                .IsRequired();

            entityTypeBuilder.Property(member => member.Provider)
                .HasColumnName("provider")
                .HasColumnType("varchar(50)")
                .HasConversion(
                    provider => provider.ToString(),
                    provider => provider.ToEnum<ProviderTypes>())
                .IsRequired();

            entityTypeBuilder.Property(member => member.Username)
                .HasColumnName("username")
                .HasColumnType("varchar(320)")
                .HasConversion(
                    username => username.Value,
                    username => new Email(username))
                .IsRequired();

            entityTypeBuilder.Property(member => member.FullName)
                .HasColumnName("full_name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            entityTypeBuilder.Property(member => member.Surname)
                .HasColumnName("surname")
                .HasColumnType("varchar(50)")
                .IsRequired();

            entityTypeBuilder.Property(member => member.GivenName)
                .HasColumnName("given_name")
                .HasColumnType("varchar(50)")
                .IsRequired();

            entityTypeBuilder.Property(member => member.ProfileUri)
                .HasColumnName("profile_uri")
                .HasColumnType("varchar(2083)")
                .IsRequired();

            entityTypeBuilder.Property(member => member.RegisteredAt)
                .HasColumnName("registered_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();
        }
    }
}