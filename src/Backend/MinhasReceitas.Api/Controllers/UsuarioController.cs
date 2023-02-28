using Microsoft.AspNetCore.Mvc;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;
using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Communication.Respostas;

namespace MinhasReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUseCase useCase, [FromBody] RequisicaoRegistrarUsuarioJson request)
    {
        var resultado = await useCase.Executar(request);

        return Created("", resultado);
    }
}