using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ErrorHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        // Ejemplo: Usar las Execiones preexistente y tomar acciones en base al tipo de error
        [HttpGet(Name = "ReadFile")]
        public IActionResult ReadFile()
        {
            try
            {
                using (StreamReader sr = System.IO.File.OpenText("data.txt"))
                {
                    return Ok($"The first line of this file is {sr.ReadLine()}");
                }
            }
            catch (FileNotFoundException e)
            {
                return StatusCode(404, $"The file was not found: '{e}'");
            }
            catch (DirectoryNotFoundException e)
            {
                return StatusCode(404, $"The directory was not found: '{e}'");
            }
            catch (IOException e)
            {
                // Ejemplo: Loguear siempre que se trate de un error
                _logger.LogError(e, $"The file could not be opened: '{e}'");
                return StatusCode(423, $"The file could not be opened: '{e}'");
            }
            catch (Exception e)
            {
                // Ejemplo: Loguear siempre que se trate de un error
                _logger.LogError(e, $"General exception: '{e}'");
                return StatusCode(500, $"General exception: '{e}'");
            }
        }


    }
}
