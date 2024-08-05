using CrawlerAceptaElReto.Models;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;

namespace CrawlerAceptaElReto.Utilities
{
    /// <summary>
    /// UtilsCrawler.
    /// </summary>
    public static class UtilsCrawler
    {
        // Dominio de la página.
        private static readonly string? urlAceptaElReto = UtilsConfig.GetUrlAceptaElReto();

        // Ruta del JavaScript que se ha de ejecutar. 
        private static readonly string rutaJS = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}JavaScript{Path.DirectorySeparatorChar}updatetable.js";

        // Listado de URLs dónde se encuentran las estadísticas.
        private static readonly List<string> listaUrls =
        [
            $"{urlAceptaElReto}/?vol=71",
            $"{urlAceptaElReto}/?vol=75",
            $"{urlAceptaElReto}/?vol=88",
            $"{urlAceptaElReto}/?vol=106",
            $"{urlAceptaElReto}/?vol=120",
            $"{urlAceptaElReto}/?vol=147",
            $"{urlAceptaElReto}/?vol=158"
        ];

        /// <summary>
        /// Recopila los datos estadísticos de la web 'Acepta el reto'.
        /// </summary>
        /// <returns>Lista de objetos ordenada según el número de usuarios / porcentaje de éxito.</returns>
        public static List<DataCrawler> Crawler()
        {
            List<DataCrawler> datos = [];

            foreach (var url in listaUrls)
            {
                HtmlDocument htmlRecurso = new();
                HtmlNodeCollection listaNodos;

                // Obtención del HTML.
                using (ChromeDriver driver = new())
                {
                    driver.Navigate().GoToUrl(url);
                    driver.ExecuteScript(File.ReadAllText(rutaJS));
                    htmlRecurso.LoadHtml(driver.PageSource);
                }

                listaNodos = htmlRecurso.DocumentNode.SelectNodes("//tbody[@id='problemsInfo-table']//tr");

                // Almacenamiento de datos.
                if (listaNodos != null && listaNodos.Count != 0)
                {
                    datos.AddRange(listaNodos.Select(nodoAux => new DataCrawler
                    {
                        Id = nodoAux.ChildNodes[1].InnerText.Trim(),
                        Name = nodoAux.ChildNodes[3].InnerText.Trim(),
                        NumeroUsuarios = int.Parse(nodoAux.ChildNodes[7].ChildNodes[0].NextSibling.ChildNodes[0].NextSibling.ChildNodes[3].InnerText.Split("/")[1].Trim()),
                        Porcentaje = int.Parse(nodoAux.ChildNodes[7].ChildNodes[0].NextSibling.ChildNodes[0].NextSibling.GetAttributeValue("title", string.Empty).Replace("%", string.Empty).Trim())
                    }));
                }
            }

            // Ordenación de los datos.
            datos = [.. datos.OrderByDescending(x => x.NumeroUsuarios / x.Porcentaje)];

            return datos;
        }
    }
}