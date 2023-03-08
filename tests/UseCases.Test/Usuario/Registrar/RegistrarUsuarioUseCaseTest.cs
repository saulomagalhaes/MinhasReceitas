using FluentAssertions;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;
using MinhasReceitas.Exceptions;
using MinhasReceitas.Exceptions.ExceptionsBase;
using UtilitarioTestes.EnciptcacaoSenha;
using UtilitarioTestes.Mapper;
using UtilitarioTestes.Repositorios;
using UtilitarioTestes.Requisicoes;
using UtilitarioTestes.Token;
using Xunit;

namespace UseCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(requisicao);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Email_Ja_Registrado()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 && exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_JA_CADASTRADO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Email = string.Empty;

        var useCase = CriarUseCase(requisicao.Email);

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 && exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));
    }

    private RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var mapper = MapperBuilder.Instancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);
    }
}
