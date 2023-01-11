using AutoMapper;
using ScannerWeb.Entities;
using ScannerWeb.Models;

namespace ScannerWeb
{
	public class ScannerMappingProfile : Profile
	{
		public ScannerMappingProfile()
		{
			CreateMap<Karty, KartyDto>()
			.ReverseMap(); //w obie strony

			CreateMap<Pracownik, PracownikDto>()
			.ReverseMap(); //w obie strony
		
			CreateMap<DodajPracownikaDto, Pracownik>()
			.ReverseMap();

			CreateMap<SkanerDto, Skaner>()
			.ReverseMap();
		}

	}
}
