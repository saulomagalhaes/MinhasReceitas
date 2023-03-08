using AutoMapper;
using MinhasReceitas.Application.Servicos.Automapper;

namespace UtilitarioTestes.Mapper;

public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutomapperConfiguracao>();
        });      

        return configuracao.CreateMapper();
    }
}
