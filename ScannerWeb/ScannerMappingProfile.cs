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
			.ReverseMap();

			CreateMap<Pracownik, PracownikDto>()
			.ReverseMap();

			CreateMap<SkanerDto, Skaner>()
			.ReverseMap();

			CreateMap<DodajHistoriumDto, Historium>()
			.ReverseMap();
		}

	}
}
