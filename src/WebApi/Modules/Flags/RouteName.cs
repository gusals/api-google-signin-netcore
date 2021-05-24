namespace WebApi.Modules.Flags
{
    /// <summary>
    /// Route Name.
    /// </summary>
    public sealed class RouteName
    {
        /// <summary>
        /// GET /users/:id
        /// </summary>
        public const string GET_USER = "GetUser";

        /// <summary>
        /// POST /authentications/google
        /// </summary>
        public const string AUTHENTICATE_GOOGLE = "AuthenticateGoogle";
    }
}