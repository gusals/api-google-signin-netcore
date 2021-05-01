using System;
using Domain.RefreshTokens;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations
{
    /// <summary>
    /// RefreshToken Configuration.
    /// </summary>
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        /// <summary>
        /// Configure RefreshToken.
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        public void Configure(EntityTypeBuilder<RefreshToken> entityTypeBuilder)
        {
            if (entityTypeBuilder == null)
                throw new ArgumentNullException(paramName: nameof(entityTypeBuilder));

            entityTypeBuilder.ToTable("refresh_token");

            entityTypeBuilder.Property(refreshToken => refreshToken.Id)
                .HasConversion(
                    id => id.Value,
                    id => new RefreshTokenId(id))
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.Value)
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.ExpiresAt)
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.CreatedByIp)
                .HasConversion(
                    createdByIp => createdByIp.Value,
                    createdByIp => new IpAddress(createdByIp))
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.CreatedAt)
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.RevokedByIp)
                .HasConversion(
                    revokedByIp => revokedByIp.Value,
                    revokedByIp => new IpAddress(revokedByIp))
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.RevokedAt)
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.MemberId)
                .HasConversion(
                    memberId => memberId.Value,
                    memberId => new MemberId(memberId))
                .IsRequired();
        }
    }
}