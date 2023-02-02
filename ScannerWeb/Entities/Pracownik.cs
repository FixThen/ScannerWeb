using System;
using System.Collections.Generic;

namespace ScannerWeb.Entities;

public partial class Pracownik
{
    public int IdPracownik { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string Pesel { get; set; } = null!;

    public string NumerTelefonu { get; set; } = null!;

    public int IdKarty { get; set; }

    public string PoziomDostepu { get; set; } = null!;

    public virtual ICollection<Historium> Historia { get; } = new List<Historium>();

    public virtual Karty IdKartyNavigation { get; set; } = null!;
}
