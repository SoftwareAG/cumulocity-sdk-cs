namespace Cumulocity.SDK.Client.Rest.Model.Authentication
{
    public sealed class CumulocityCredentials
    {
        public CumulocityCredentials(string username, string password) : this(new CumulocityLogin(null, username),
            password, null)
        {
        }

        public CumulocityCredentials(string tenantId, string username, string password, string applicationKey) : this(
            new CumulocityLogin(tenantId, username), password, applicationKey)
        {
        }

        public CumulocityCredentials(CumulocityLogin login, string password, string applicationKey) : this(login,
            password, null, null, applicationKey, null)
        {
        }

        public CumulocityCredentials(CumulocityLogin login, string password, string oAuthAccessToken, string xsrfToken,
            string applicationKey, string requestOrigin)
        {
            Login = login;
            Password = password;
            ApplicationKey = applicationKey;
            RequestOrigin = requestOrigin;
            OAuthAccessToken = oAuthAccessToken;
            XsrfToken = xsrfToken;
        }

        public CumulocityLogin Login { get; }

        public string TenantId => Login.TenantId;

        public string Username => Login.Username;

        public string Password { get; }

        public string OAuthAccessToken { get; }

        public string XsrfToken { get; }

        public string ApplicationKey { get; }

        public string RequestOrigin { get; }

        public sealed class Builder
        {
            private readonly string password;

            private string applicationKey;
            private CumulocityLogin login;

            private string oAuthAccessToken;

            private string requestOrigin;

            private string xsrfToken;

            public Builder(string user, string password)
            {
                login = new CumulocityLogin(null, user);
                this.password = password;
            }

            public static Builder cumulocityCredentials(string user, string password)
            {
                return new Builder(user, password);
            }

            public Builder withOAuthAccessToken(string oAuthAccessToken)
            {
                this.oAuthAccessToken = oAuthAccessToken;
                return this;
            }

            public Builder withXsrfToken(string xsrfToken)
            {
                this.xsrfToken = xsrfToken;
                return this;
            }

            public Builder withApplicationKey(string applicationKey)
            {
                this.applicationKey = applicationKey;
                return this;
            }

            public Builder withRequestOrigin(string requestOrigin)
            {
                this.requestOrigin = requestOrigin;
                return this;
            }

            public Builder withTenantId(string tenantId)
            {
                login = new CumulocityLogin(tenantId, login.Username);
                return this;
            }

            public CumulocityCredentials build()
            {
                return new CumulocityCredentials(login, password, oAuthAccessToken, xsrfToken, applicationKey,
                    requestOrigin);
            }
        }
    }
}