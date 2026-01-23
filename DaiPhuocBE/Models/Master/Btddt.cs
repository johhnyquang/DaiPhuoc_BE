using System;
using System.Collections.Generic;

namespace DaiPhuocBE.Models.Master;

public partial class Btddt
{
    public string Madantoc { get; set; } = null!;

    public string? Dantoc { get; set; }

    public DateTime? Ngayud { get; set; }

    public byte? IdSytdantoc { get; set; }

    public bool? Hide { get; set; }

    public string? Mabh { get; set; }
}
