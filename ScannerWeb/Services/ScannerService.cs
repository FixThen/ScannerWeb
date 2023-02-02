using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ScannerWeb.Entities;
using ScannerWeb.Models;
using System;
using System.Security.Cryptography;

namespace ScannerWeb.Services
{
    public interface IScannerService
	{
		SkanerDto? SprawdzenieSkanera(int idSkanera);
		string? TypAutoryzacji(SkanerDto dto, String type);
		IEnumerable<PracownikDto> GetAll();
		void DodajH(int idSkanera, int idOsoby, String type);
		int Autoryzacja(String type, int password);
		int SprawdzeniePoziomuDostepu(SkanerDto dto, int uidOsoby);
	}

	public class ScannerService : IScannerService
	{
		private readonly ScannerDbContext _dbContext;
		private readonly IMapper _mapper;
		
		public ScannerService(ScannerDbContext dbContext, IMapper mapper)
		{
			// baza danych
			_dbContext = dbContext;
			// mappowanie/przenoszenie do bazy
			_mapper = mapper;
		}
		public IEnumerable<PracownikDto> GetAll()
		{
			var pracowniks = _dbContext
				.Pracowniks
				.ToList();
			
			var pracowniksDtos = _mapper.Map<List<PracownikDto>>(pracowniks);

			return pracowniksDtos;
		}
		// Sprawdzenie czy skaner o podanym identyfikatorze istnieje w bazie danych
		public SkanerDto? SprawdzenieSkanera(int idSkanera)
		{
			var skaner = _dbContext
				.Skaners
				.FirstOrDefault(k => k.IdSkanera == idSkanera);
			if (skaner is null) return null;
			var result = _mapper.Map<SkanerDto>(skaner);
			return result;
		}
		
		// Sprawdzenie czy skaner posiada autoryzację za pomocą danego typu
		public string? TypAutoryzacji(SkanerDto dto, String typ)
		{
			if(!(typ == "UID" || typ == "KOD")) return null;
			var typ_2 = dto.TypAutoryzacji;
			if (typ_2 == "UK")
				return ("ok");
			else if (typ_2 == typ)
				return ("ok");
			else
				return null;
		}
		// Sprawdzenie czy dane do autoryzacji są prawidłowe
		public int Autoryzacja(String typ, int haslo)
		{
			Karty karta;
			if (typ == "KOD")
			{
				karta = _dbContext.Karties.FirstOrDefault(r => r.KodOtwarcia == haslo);
			}
			else if (typ == "UID")
			{
				karta = _dbContext.Karties.FirstOrDefault(r => r.Uid == haslo);
			}
			else
			{
				return 0;
			}
			if (karta != null)
			{
				return karta.Uid;
			}
			else
			{
				return 0;
			}
		}
		// Sprawdzenie czy osoba posiada dostęp do danego budynku
		public int SprawdzeniePoziomuDostepu(SkanerDto dto, int uidOsoby)	
		{
			var poziom = dto.PoziomDostepu;
			var pracownik = _dbContext.Pracowniks.FirstOrDefault(k => k.IdKarty == uidOsoby);
			if (pracownik == null) return 0;
			return pracownik.PoziomDostepu switch
			{
				"A" => pracownik.IdPracownik,
				"B" when poziom == "B" || poziom == "C" => pracownik.IdPracownik,
				"C" when poziom == "C" => pracownik.IdPracownik,
				_ => 0,
			};
		}
		// Dodanie nowego rekordu z osobą do bazy danych 
		public void DodajH(int idSkanera, int idOsoby, String type)
		{
			var historia = new DodajHistoriumDto();
			historia.IdSkanera = idSkanera;
			historia.IdOsoby = idOsoby;
			historia.TypAutoryzacji = type;
			_dbContext.Historia.Add(_mapper.Map<Historium>(historia));
			_dbContext.SaveChanges();
		}
	}
}

