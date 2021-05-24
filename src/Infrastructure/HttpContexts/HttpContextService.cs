using System.Linq;
using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.HttpContexts
{
    /// <inheritdoc />
    public sealed class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <inheritdoc />
        public HttpContextService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        /// <inheritdoc />
        public string GetUserAgent() =>
                _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].FirstOrDefault()
                ?? string.Empty;

        /// <inheritdoc />
        public string GetIpAddress() =>
            _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault()
            ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            ?? string.Empty;
    }
}