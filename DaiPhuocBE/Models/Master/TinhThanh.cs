namespace DaiPhuocBE.Models.Master
{
    public partial class TinhThanh
    {
        public string MaTinhThanh { get; set; } = null!;
        public string TenTinhThanh { get; set;} = null!;
        public string? VietTat { get;set; }
        public int Hide { get; set; }= 0;
    }
}
