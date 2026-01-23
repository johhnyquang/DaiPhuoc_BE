using System;
using System.Collections.Generic;

namespace DaiPhuocBE.Models.Master;

public partial class Phuongxa
{
    public string Maphuongxa { get; set; } = null!;

    public string Tenphuongxa { get; set; } = null!;

    public string? Viettat { get; set; }

    public bool? Hide { get; set; }
}
