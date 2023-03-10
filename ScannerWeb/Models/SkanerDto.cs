using ScannerWeb.Entities;

namespace ScannerWeb.Models
{
    public class SkanerDto
	{
			public int IdSkanera { get; set; }

			public int IdBudynku { get; set; }

			public string? TypAutoryzacji { get; set; }

			public int? KodOtwarcia { get; set; }

			public string? PoziomDostepu { get; set; }

			public virtual ICollection<Historium> Historia { get; } = new List<Historium>();

			public virtual Budynek IdBudynkuNavigation { get; set; } = null!;
	}
}
