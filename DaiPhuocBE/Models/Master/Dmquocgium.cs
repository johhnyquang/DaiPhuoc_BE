using System;
using System.Collections.Generic;

namespace DaiPhuocBE.Models.Master;

public partial class Dmquocgium
{
    public string Ma { get; set; } = null!;

    public string? Ten { get; set; }

    public string? Valuea { get; set; }

    public string? Vietnamese { get; set; }

    public DateTime? Ngayud { get; set; }

    public byte? IdSytquocgia { get; set; }

    public string? Mabh { get; set; }
}
