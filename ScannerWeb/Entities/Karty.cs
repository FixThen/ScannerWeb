using System;
using System.Collections.Generic;

namespace ScannerWeb.Entities;

public partial class Karty
{
    public int Uid { get; set; }

    public int IdOsoby { get; set; }

    public int? KodOtwarcia { get; set; }

    public virtual ICollection<Pracownik> Pracowniks { get; } = new List<Pracownik>();
}
