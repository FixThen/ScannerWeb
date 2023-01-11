using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScannerWeb.Entities;
using ScannerWeb.Models;
using ScannerWeb.Services;

namespace ScannerWeb.Controller
{
	[Route("api/scanner")]
	public class ScannerController : ControllerBase
	{
		private readonly IScannerService _scannerService;
		public ScannerController(IScannerService scannerService)
		{
			_scannerService = scannerService;
		}

		[HttpGet]
		public ActionResult<IEnumerable<PracownikDto>> GetAll()
		{
			var pracownikDto = _scannerService.GetAll();
			return Ok(pracownikDto);
		}
		[HttpGet("{idSkanera}/UID/{uid}")]
		public ActionResult<KartyDto> Get([FromRoute]int idSkanera, int uid)
		{
		var karta = _scannerService.GetByUid(uid);
		var skaner = _scannerService.SprawdzenieSkanera(idSkanera);
		if (skaner is null) return NotFound("Nie znaleziono takiego skanera");
		if (karta is null) return NotFound("Nie znaleziono takiej karty");
		return Ok(karta);
		}
		[HttpGet("{idSkanera}/KOD/{kodOtwarcia}")]
		public ActionResult<KartyDto> sprawdzKOD([FromRoute] int idSkanera, int kodOtwarcia)
		{
			var karta = _scannerService.GetByKOD(kodOtwarcia);
			var skaner = _scannerService.SprawdzenieSkanera(idSkanera);
			if (skaner is null) return NotFound("Nie znaleziono takiego skanera");
			if (karta is null) return NotFound("Nie znaleziono takiego kodu");
			return Ok(karta);
		}
		//{idSkanera}
		[HttpPost]
		// wysłanie danych przez klijenta 
		public ActionResult DodajPracownika([FromBody] DodajPracownikaDto dto)
		{
			//sprawdzenie właściwości 
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var id = _scannerService.Dodaj(dto);
			return Created("/api/scanner/{id}", null);
		}
	}
}
