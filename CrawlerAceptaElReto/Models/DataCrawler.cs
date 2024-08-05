namespace CrawlerAceptaElReto.Models
{
    /// <summary>
    /// Clase DataCrawler.
    /// </summary>
    public class DataCrawler
    {
        /// <summary>
        /// Almacena el ID del problema.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Almacena el nombre del problema.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Almacena el número de usuarios que han intentado el problema.
        /// </summary>
        public int? NumeroUsuarios { get; set; }

        /// <summary>
        /// Almacena el porcentaje de éxito del problema.
        /// </summary>
        public int? Porcentaje { get; set; }
    }
}
