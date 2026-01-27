namespace DaiPhuocBE.DTOs.AuthDTOs
{
    public class RotateModel
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string? IpAddress { get; set; }
        public string? DeviceId { get; set; }
    }
}
