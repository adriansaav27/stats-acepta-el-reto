namespace CrawlerAceptaElReto.Utilities
{
	/// <summary>
	/// Clase UtilsConfig.
	/// </summary>
	public static class UtilsConfig
	{
		// Archivo de configuración.
		private static readonly IConfigurationRoot configuracion = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();

		/// <summary>
		/// Obtiene la URL de Acepta el Reto.
		/// </summary>
		/// <returns>URL de Acepta el Reto.</returns>
		public static string? GetUrlAceptaElReto()
		{
			var environmentVariables = Environment.GetEnvironmentVariables();

			return environmentVariables.Contains("UrlAceptaElReto")
				? environmentVariables["UrlAceptaElReto"] as string
				: configuracion["UrlAceptaElReto"];
		}
	}
}
