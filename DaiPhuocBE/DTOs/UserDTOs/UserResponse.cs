namespace DaiPhuocBE.DTOs.UserDTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Hoten { get; set; } = string.Empty;
        public string Socmnd { get; set; }  = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public DateTime Ngaysinh { get; set; } 
        public string Phai { get; set; } = string.Empty;
        public string DanToc { get; set; } = string.Empty;
        public string QuocTich { get; set; } = string.Empty;
        public string TinhThanh { get; set; } = string.Empty;
        public string PhuongXa { get; set; } = string.Empty;
    }
}
