using AutoMapper;
using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Domain.Repositorios;
using MinhasReceitas.Exceptions.ExceptionsBase;

namespace MinhasReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
{
    private readonly IUsuarioWriteOnlyRepositorio _repositorio;
    private readonly IMapper _mapper;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorio, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorio = repositorio;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(RequisicaoRegistrarUsuarioJson requisicao)
    {
        Validar(requisicao);

        var usuario = _mapper.Map<Domain.Entidades.Usuario>(requisicao);
        usuario.Senha = "cript";

        await _repositorio.Adicionar(usuario);

        await _unidadeDeTrabalho.Commit();
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
