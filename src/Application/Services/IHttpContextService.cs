namespace Application.Services
{
    /// <summary>
    /// HttpContext Service.
    /// </summary>
    public interface IHttpContextService
    {
        /// <summary>
        /// Get UserAgent.
        /// </summary>
        /// <returns></returns>
        string GetUserAgent();

        /// <summary>
        /// Get IpAddress.
        /// </summary>
        /// <returns></returns>
        string GetIpAddress();
    }
}