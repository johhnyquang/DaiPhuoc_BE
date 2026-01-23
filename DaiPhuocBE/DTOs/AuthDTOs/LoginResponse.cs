namespace DaiPhuocBE.DTOs.AuthDTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
