using AutoMapper;
using FluentValidation.Results;
using MinhasReceitas.Application.Servicos.Criptografia;
using MinhasReceitas.Application.Servicos.Token;
using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Communication.Respostas;
using MinhasReceitas.Domain.Repositorios;
using MinhasReceitas.Exceptions;
using MinhasReceitas.Exceptions.ExceptionsBase;

namespace MinhasReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioWriteOnlyRepositorio _repositorioEscrita;
    private readonly IUsuarioReadOnlyRepositorio _repositorioLeitura;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio repositorioLeitura)
    {
        _repositorioEscrita = repositorio;
        _repositorioLeitura = repositorioLeitura;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        await Validar(requisicao);

        var usuario = _mapper.Map<Domain.Entidades.Usuario>(requisicao);
        usuario.Senha = _encriptadorDeSenha.Criptografar(requisicao.Senha);

        await _repositorioEscrita.Adicionar(usuario);
        await _unidadeDeTrabalho.Commit();

        var token = _tokenController.GerarToken(usuario.Email);
        return new RespostaUsuarioRegistradoJson
        {
            Token = token
        };

    }

    private async Task Validar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        var resultado = new RegistrarUsuarioValidator().Validate(requisicao);

        var existeUsuarioComEmail = await _repositorioLeitura.ExisteUsuarioComEmail(requisicao.Email);
        if (existeUsuarioComEmail)
        {
            resultado.Errors.Add(new ValidationFailure("Email", ResourceMensagensDeErro.EMAIL_JA_CADASTRADO));
        }

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
