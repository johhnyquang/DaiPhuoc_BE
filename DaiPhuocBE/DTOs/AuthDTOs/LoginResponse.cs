namespace DaiPhuocBE.DTOs.AuthDTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Mabn { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
}
