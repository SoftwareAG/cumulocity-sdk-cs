namespace Cumulocity.SDK.Client.Rest.Model.Authentication
{
    public sealed class CumulocityLogin
    {
        private const char SEPARATOR = '/';

        internal CumulocityLogin(string tenantId, string username)
        {
            TenantId = tenantId;
            Username = username;
        }

        public string TenantId { get; }

        public string Username { get; }

        public static CumulocityLogin fromLoginString(string loginString)
        {
            var usernameIndex = ReferenceEquals(loginString, null) ? -1 : loginString.IndexOf(SEPARATOR);

            if (usernameIndex < 0) return new CumulocityLogin(null, loginString);

            var tenantId = loginString.Substring(0, usernameIndex);
            var username = loginString.Substring(usernameIndex + 1);
            return new CumulocityLogin(tenantId, username);
        }

        public string toLoginString()
        {
            if (ReferenceEquals(TenantId, null))
                return Username;
            return TenantId + SEPARATOR + Username;
        }
    }
}