namespace DaiPhuocBE.DTOs.AuthDTOs
{
    public class Register
    {
        public string CCCD { get; set; } = string.Empty;
        public string HOTEN { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? NgaySinh { get; set; }
        public bool? Phai { get; set; }
        public string QuocTich { get; set; } = string.Empty;
        public string DanToc { get; set; } = string.Empty;
        public string TinhThanh { get;set; } = string.Empty;
        public string PhuongXa { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? IpAddress { get; set; }
        public string? DeviceId { get; set; }

    }
}
