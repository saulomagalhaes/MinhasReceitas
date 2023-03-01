using FluentAssertions;
using MinhasReceitas.Application.UseCases.Usuario.Registrar;
using MinhasReceitas.Exceptions;
using UtilitarioTestes.Requisicoes;
using Xunit;

namespace Validators.Test.Usuario.Registrar;

public class RegistrarUsuarioValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_Nome_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Nome = "";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Email = "";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Email = "emailinvalido";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazia()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Senha = "";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalida(int tamanhoSenha)
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));
    }   

    [Fact]
    public void Validar_Erro_Telefone_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Telefone = "";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistrarUsuarioBuilder.Construir();
        requisicao.Telefone = "123456789";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
    }
}
