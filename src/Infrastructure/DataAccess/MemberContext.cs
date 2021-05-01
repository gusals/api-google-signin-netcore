using System;
using Domain.Members;
using Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    /// <summary>
    /// Member Db Context.
    /// </summary>
    public sealed class MemberContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberContext" /> class.
        /// </summary>
        /// <param name="options"></param>
        public MemberContext(DbContextOptions options) : base(options: options) { }

        /// <summary>
        /// Member Entity.
        /// </summary>
        public DbSet<Member> Members { get; set; } = null!;

        /// <summary>
        /// RefreshToken Entity.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(paramName: nameof(builder));

            builder.ApplyConfigurationsFromAssembly(assembly: typeof(MemberContext).Assembly);
            SeedData.Seed(builder: builder);
        }
    }
}