using CrawlerAceptaElReto.Models;
using CrawlerAceptaElReto.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CrawlerAceptaElReto.Controllers
{
	/// <summary>
	/// Controlador.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	[EnableCors("AllowCors")]
	public class AceptaElRetoController : ControllerBase
	{
		/// <summary>
		/// Genera el ránking de problemas de 'Acepta el reto'.
		/// </summary>
		/// <returns>Lista ordenada según la dificultad de los problemas y porcentaje de éxito de los usuarios.</returns>
		[Route("[action]")]
		[HttpGet]
		public ActionResult<List<DataCrawler>> GenerarRanking()
		{
			try
			{
				List<DataCrawler> resultado = UtilsCrawler.Crawler();
				if (resultado == null || resultado.Count == 0) return NoContent();
				return Ok(resultado);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
	}
}