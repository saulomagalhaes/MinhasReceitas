using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Exceptions.ExceptionsBase;

namespace MinhasReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase
{
    public async Task Executar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        Validar(requisicao);

    }

    private void Validar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        var resultado = new RegistrarUsuarioValidator().Validate(requisicao);

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
