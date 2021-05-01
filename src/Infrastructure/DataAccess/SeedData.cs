using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    /// <summary>
    /// Data seeding is the process of populating a database with an initial set of data.
    /// </summary>
    public static class SeedData
    {
        /// <inheritdoc />
        public static void Seed(ModelBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(paramName: nameof(builder));
        }
    }
}