namespace DaiPhuocBE.DTOs.AuthDTOs
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string? IpAddress { get; set; }
        public string? DeviceId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
