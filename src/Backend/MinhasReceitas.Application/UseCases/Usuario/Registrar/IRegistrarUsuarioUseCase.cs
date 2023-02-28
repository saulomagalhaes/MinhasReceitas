using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Communication.Respostas;

namespace MinhasReceitas.Application.UseCases.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
}
