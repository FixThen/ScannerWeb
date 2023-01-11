using AutoMapper;
using ScannerWeb.Entities;
using ScannerWeb.Models;

namespace ScannerWeb.Services
{
	public interface IScannerService
	{
		SkanerDto SprawdzenieSkanera(int idSkanera);
		KartyDto GetByUid(int uid);
		KartyDto GetByKOD(int kodOtwarcia);
		IEnumerable<PracownikDto> GetAll();
		int Dodaj(DodajPracownikaDto dto);
		//ScannerDto GetById(int id);
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
				//.Include(s => s.Employees)
				.ToList();

			var pracowniksDtos = _mapper.Map<List<PracownikDto>>(pracowniks);

			return pracowniksDtos;
		}
		public int Dodaj(DodajPracownikaDto dto)
		{
			var pracownik = _mapper.Map<Pracownik>(dto);
			_dbContext.Pracowniks.Add(pracownik);
			_dbContext.SaveChanges();
			return pracownik.IdPracownik;
		}
		public SkanerDto SprawdzenieSkanera(int idSkanera)
		{
			var skaner = _dbContext
				.Skaners
				.FirstOrDefault(k => k.IdSkanera == idSkanera);
			if (skaner is null) return null;
			var result = _mapper.Map<SkanerDto>(skaner);
			return result;
		}
		public KartyDto GetByUid(int uid)
		{
			var karta = _dbContext
				.Karties
				//.Include(s => s.Osoby)
				.FirstOrDefault(r => r.Uid == uid);
			
			if (karta is null) return null;
			var result = _mapper.Map<KartyDto>(karta);
			return result;
		}
		public KartyDto GetByKOD(int kodOtwarcia)
		{
			var kod = _dbContext
				.Karties
				.FirstOrDefault(r => r.KodOtwarcia == kodOtwarcia);
				//r => r.Id == id
			if (kod is null) return null;
			var result = _mapper.Map<KartyDto>(kod);
			return result;
		}
	}
}
