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
                .HasColumnName("id")
                .HasColumnType("bigint(20)")
                .HasConversion(
                    id => id.Value,
                    id => new RefreshTokenId(id))
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("bigint(20)")
                .HasConversion(
                    memberId => memberId.Value,
                    memberId => new MemberId(memberId))
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.Value)
                .HasColumnName("value")
                .HasColumnType("varchar(500)")
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.ExpiresAt)
                .HasColumnName("expires_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.CreatedByIp)
                .HasColumnName("created_by_ip")
                .HasColumnType("varchar(15)")
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("current_timestamp")
                .IsRequired();

            entityTypeBuilder.Property(refreshToken => refreshToken.RevokedByIp)
                .HasColumnName("revoked_by_ip")
                .HasColumnType("varchar(15)");

            entityTypeBuilder.Property(refreshToken => refreshToken.RevokedAt)
                .HasColumnName("revoked_at")
                .HasColumnType("datetime");
        }
    }
}