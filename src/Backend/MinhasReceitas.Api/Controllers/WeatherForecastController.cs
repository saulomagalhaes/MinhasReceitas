using Microsoft.AspNetCore.Mvc;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;
using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Exceptions;

namespace MinhasReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var usecase = new RegistrarUsuarioUseCase();
        await usecase.Executar(new RequisicaoRegistrarUsuarioJson
        {
            
        });

        return Ok();
    }
}