namespace AspNetCoreGettingStarted.Configuration
{
    public class SeedDataSettings
    {
        public bool Reload { get; set; }
    }

    public class AuthenticationSettings
    {
        public string TokenPath { get; set; }
        public int ExpirationMinutes { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string AuthType { get; set; }
    }
}
