using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScannerWeb.Entities;
using ScannerWeb.Models;
using ScannerWeb.Services;

namespace ScannerWeb.Controller
{
	// Określenie ścieżki, pod którą będą dostępne akcje kontrolera
	[Route("api/scanner")]
	public class ScannerController : ControllerBase
	{
		// Pole typu interfejsu IScannerService, do którego będzie można uzyskać dostęp w kontrolerze
		private readonly IScannerService _scannerService;

		// Konstruktor kontrolera, który przyjmuje jako parametr obiekt typu IScannerService
		public ScannerController(IScannerService scannerService)
		{
			_scannerService = scannerService;
		}
		// Akcja, która będzie dostępna pod ścieżką api/scanner i będzie zwracać listę wszystkich pracowników
		[HttpGet]
		public ActionResult<IEnumerable<PracownikDto>> GetAll()
		{
			// Pobranie listy wszystkich pracowników za pomocą metody GetAll z obiektu _scannerService
			var pracownikDto = _scannerService.GetAll();
			// Zwrócenie odpowiedzi 200 (OK) zawierającej listę pracowników
			return Ok(pracownikDto);
		}
		// Akcja, która będzie dostępna pod ścieżką api/scanner/{idSkanera}/{type}/{password}
		[HttpGet("{idSkanera}/{type}/{password}")]
		public ActionResult Przetwarzanie([FromRoute] int idSkanera,String type, int password)
		{
			// Sprawdzenie czy stan modelu jest prawidłowy
			if (!ModelState.IsValid)
			{
				// Jeśli nie jest prawidłowy, zwróć odpowiedź 400 (Bad Request) zawierającą błędy w modelu
				return BadRequest(ModelState);
			}
			// Pobranie obiektu skanera za pomocą metody SprawdzenieSkanera z obiektu _scannerService
			var skaner = _scannerService.SprawdzenieSkanera(idSkanera);
			if (skaner is null) return NotFound("Nie znaleziono takiego skanera");
			var typ = _scannerService.TypAutoryzacji(skaner, type);
			if (typ is null) return NotFound("Skaner nie posiada autoryzacji za pomocą " + type);
			var uid = _scannerService.Autoryzacja(type,password);
			if (uid == 0) return NotFound("Autoryzacja nie przebiegła pomyślnie");
			var idOsoby = _scannerService.SprawdzeniePoziomuDostepu(skaner, uid);
			if (idOsoby == 0) return NotFound("Osoba nie posiada dostępu do tego budynku");
			_scannerService.DodajH(idSkanera, idOsoby, type);
			return Ok("otwarte");
		}

	}
}
