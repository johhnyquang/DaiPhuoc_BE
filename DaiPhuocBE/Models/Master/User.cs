using System;
using System.Collections.Generic;

namespace DaiPhuocBE.Models.Master;

public partial class User
{
    public int Id { get; set; }
    public string Socmnd { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Sdt { get; set; } = null!;
    public string Hoten { get; set; } = null!;
    public DateTime? Ngaysinh { get; set; }
    public string? Namsinh { get; set; }
    public bool Phai { get; set; }
    public string quoctich { get; set; } = null!;
    public string dantoc { get; set; } = null!;
    public string matinh { get; set; } = null!;
    public string maphuongxa { get; set; } = null!;
    public string? Email { get; set; }
}
