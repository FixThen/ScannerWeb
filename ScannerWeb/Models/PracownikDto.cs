using ScannerWeb.Entities;

namespace ScannerWeb.Models
{
    public class PracownikDto
	{
		public int IdPracownik { get; set; }

		public string Imie { get; set; } = null!;

		public string Nazwisko { get; set; } = null!;

		public string Pesel { get; set; } = null!;

		public string? NumerTelefonu { get; set; }

		public int? IdKarty { get; set; }

		public string? PoziomDostepu { get; set; }

		public virtual ICollection<Historium> Historia { get; } = new List<Historium>();

		public virtual Karty? IdKartyNavigation { get; set; }
	}
}
