namespace DaiPhuocBE.DTOs.CustomerDTOs
{
    public class CustomerResponse
    {
        public string Mabn { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public bool Phai { get; set; } 
        public string SoCmnd { get; set; } = string.Empty;
        public string BHYT { get; set; } = string.Empty;
    }
}
