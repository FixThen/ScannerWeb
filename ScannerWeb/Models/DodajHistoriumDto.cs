using ScannerWeb.Entities;

namespace ScannerWeb.Models
{
    public class DodajHistoriumDto
	{
		public int Id { get; set; }
		public int IdSkanera { get; set; }

		public int IdOsoby { get; set; }

		public DateTime Data { get; set; } = DateTime.Now.Date;
		public TimeSpan Czas { get; set; } = DateTime.Now.TimeOfDay;
		public string TypAutoryzacji { get; set; } = null!;

		public virtual Pracownik IdOsobyNavigation { get; set; } = null!;

		public virtual Skaner IdSkaneraNavigation { get; set; } = null!;
	}
}
