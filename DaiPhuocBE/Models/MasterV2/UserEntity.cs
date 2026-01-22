namespace DaiPhuocBE.Models.MasterV2
{
    public class UserEntity
    {
        public string MaBn { get; set; } = null!;
        public string Socmnd { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string SDT { get; set; } = null!;
        public string Ho { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public bool Phai { get; set; }
        public string? Email { get; set; }
    }
}
