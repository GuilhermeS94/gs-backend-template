using GS.Backend.Dominios.Comandos;
using GS.Backend.Dominios.Idiomas;
using GS.Backend.Dominios.Modelos.Resultados;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GS.Backend.Ms.Controllers;

[ApiController]
[Route("healthcheck")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IStringLocalizer _idioma;
    private readonly IConfiguration _configs;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, IStringLocalizer<UsarIdioma> idioma, IConfiguration configs)
    {
        _logger = logger;
        _mediator = mediator;
        _idioma = idioma;
        _configs = configs;
    }

    [HttpPost("testar")]
    public async Task<IActionResult> PostTestar([FromBody] TestarComando comando)
    {
        TestarResultado saida = await _mediator.Send(comando);

        return Ok(saida);
    }
}

