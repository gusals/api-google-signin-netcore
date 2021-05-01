namespace WebApi.Modules.Flags
{
    /// <summary>
    /// Route Name.
    /// </summary>
    public sealed class RouteName
    {
        /// <summary>
        /// GET /members/:id
        /// </summary>
        public const string GET_MEMBER = "GetMember";

        /// <summary>
        /// POST /logins/google
        /// </summary>
        public const string GOOGLE_LOGIN = "GoogleLogin";
    }
}