using System;
using System.Collections.Generic;

namespace DaiPhuocBE.Models.Master;

public partial class Tinhthanh
{
    public string Matinhthanh { get; set; } = null!;

    public string Tentinhthanh { get; set; } = null!;

    public string? Viettat { get; set; }

    public bool? Hide { get; set; }
}
