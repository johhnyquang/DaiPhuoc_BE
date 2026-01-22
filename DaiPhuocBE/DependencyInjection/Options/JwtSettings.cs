namespace DaiPhuocBE.DependencyInjection.Options
{
    public class JwtSettings
    {
        public string SerectKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int AccessTokenExpiryMinutes { get; set; } = 60;
        public int RefreshTokenExpiryDays { get; set; } = 7;
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; } = true;
    }
}
