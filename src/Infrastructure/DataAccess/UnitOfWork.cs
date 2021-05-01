using System.Threading.Tasks;
using Application.Services;

namespace Infrastructure.DataAccess
{
    /// <inheritdoc />
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly MemberContext _memberContext;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="memberContext">Member Db Context.</param>
        public UnitOfWork(MemberContext memberContext) => _memberContext = memberContext;

        /// <inheritdoc />
        public void Dispose() => Dispose(disposing: true);

        /// <inheritdoc />
        public async Task<int> Save() =>
            await _memberContext.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _memberContext.Dispose();
            _disposed = true;
        }
    }
}