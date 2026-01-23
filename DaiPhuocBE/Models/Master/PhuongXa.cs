namespace DaiPhuocBE.Models.Master
{
    public partial class PhuongXa
    {
        public string MaPhuongXa { get; set; } = null!;
        public string TenPhuongXa { get; set; } = null!;
        public string? VietTat { get; set; }    
        public int Hide { get; set; } = 0;
    }
}
