using Microsoft.AspNetCore.Mvc;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;
using MinhasReceitas.Communication.Requisicoes;

namespace MinhasReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get([FromServices] IRegistrarUsuarioUseCase useCase)
    {
        var resposta = await useCase.Executar(new RequisicaoRegistrarUsuarioJson
        {
            Email = "sauloibotirama@hotmail.com",
            Nome = "Saulo", 
            Senha = "123456",
            Telefone = "77 9 9875-0664"
        });

        return Ok(resposta);
    }
}