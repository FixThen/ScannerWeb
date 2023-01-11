using System;
using System.Collections.Generic;

namespace ScannerWeb.Entities;

public partial class Historium
{
    public int Id { get; set; }

    public int IdSkanera { get; set; }

    public int IdOsoby { get; set; }

    public DateTime Data { get; set; }

    public TimeSpan Czas { get; set; }

    public string TypAutoryzacji { get; set; } = null!;

    public virtual Pracownik IdOsobyNavigation { get; set; } = null!;

    public virtual Skaner IdSkaneraNavigation { get; set; } = null!;
}
