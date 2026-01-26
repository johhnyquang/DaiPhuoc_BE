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

    public string Quoctich { get; set; } = null!;

    public string Dantoc { get; set; } = null!;

    public string Matinh { get; set; } = null!;

    public string Maphuongxa { get; set; } = null!;

    public string? Email { get; set; }
}
