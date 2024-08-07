# CrawlerAceptaElReto

Este proyecto es una aplicación ASP.NET Core que recopila datos estadísticos de la web 'Acepta el Reto' y genera un ranking de problemas basado en la dificultad y porcentaje de éxito de los usuarios.

## Contenido

- [Instalación](#instalación)
- [Uso](#uso)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [API](#api)
- [Licencia](#licencia)

## Instalación

1. **Clonar el repositorio**

  ```bash
  git clone https://github.com/tu_usuario/CrawlerAceptaElReto.git
  cd CrawlerAceptaElReto
  ```
   
2. **Restaurar dependencias**

  ```bash
  dotnet restore
  ```

3. **Configuración**

Crear un archivo appsettings.json en la raíz del proyecto con el siguiente contenido:
  
  ```json    
  {
    "UrlAceptaElReto": "https://aceptaelreto.com/problems/volumes.php"
  }    
  ```

4. **Ejecutar la aplicación**

  ```bash
  dotnet run
  ```

## Uso

Después de iniciar la aplicación, puedes acceder al endpoint para generar el ranking de problemas. El endpoint principal es:

- `GET /AceptaElReto/GenerarRanking`

Este endpoint devuelve una lista ordenada de problemas según la dificultad y porcentaje de éxito de los usuarios.

## Estructura del Proyecto

- **Controllers**
  - `AceptaElRetoController.cs`: Controlador principal que maneja las solicitudes HTTP.

- **Models**
  - `DataCrawler.cs`: Modelo que representa los datos de un problema.

- **Utilities**
  - `UtilsCrawler.cs`: Clase que contiene la lógica para recopilar y procesar los datos de la web 'Acepta el Reto'.
  - `UtilsConfig.cs`: Clase que maneja la configuración de la aplicación.

## API

![image](https://github.com/user-attachments/assets/f42e8db9-a5f1-4c4b-85fa-890ba889aa7d)

### `GET /AceptaElReto/GenerarRanking`

Genera el ranking de problemas de 'Acepta el Reto'.

#### Respuesta Exitosa (200 OK)
Devuelve una lista ordenada de objetos `DataCrawler`:

```json
[
  {
    "id": "string",
    "name": "string",
    "numeroUsuarios": 0,
    "porcentaje": 0
  }
]
```

####  Respuesta Sin Contenido (204 No Content) 
No se encontraron problemas para generar el ranking.

####  Respuesta de Error (500 Internal Server Error)
Devuelve un mensaje de error si ocurrió algún problema al generar el ranking:

```json
{
  "message": "string"
}
```

## Licencia

Este proyecto está licenciado bajo la Licencia [MIT](https://github.com/skynette/real-estate/blob/main/LICENSE). Consulta el archivo LICENSE para obtener más detalles.

