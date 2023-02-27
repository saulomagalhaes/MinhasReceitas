using MinhasReceitas.Communication.Requisicoes;

namespace MinhasReceitas.Application.UseCases.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task Executar(RequisicaoRegistrarUsuarioJson requisicao);
}
