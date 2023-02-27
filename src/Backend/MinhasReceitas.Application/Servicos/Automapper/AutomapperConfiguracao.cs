using AutoMapper;
using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Domain.Entidades;

namespace MinhasReceitas.Application.Servicos.Automapper;

public class AutomapperConfiguracao : Profile
{
    public AutomapperConfiguracao()
    {
        CreateMap<RequisicaoRegistrarUsuarioJson, Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore()); 
    }
}
