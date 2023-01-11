using ScannerWeb.Entities;

namespace ScannerWeb.Models
{
	public class KartyDto
	{
		public int Uid { get; set; }

		public int IdOsoby { get; set; }

		public int? KodOtwarcia { get; set; }

		public virtual ICollection<Pracownik> Pracowniks { get; } = new List<Pracownik>();
	}
}
