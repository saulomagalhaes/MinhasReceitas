using Microsoft.Extensions.Configuration;

namespace MinhasReceitas.Domain.Extension;

public static class RepositoryExtension
{
    public static string GetNomeDatabase(this IConfiguration configurationManager)
    {
        var nomeDatabase = configurationManager.GetConnectionString("NomeDatabase");

        return nomeDatabase;
    }

    public static string GetConexao(this IConfiguration configurationManager)
    {
        var conexao = configurationManager.GetConnectionString("Conexao");

        return conexao;
    }

    public static string GetConexaoCompleta(this IConfiguration configurationManager)
    {
        var nomeDatabase = configurationManager.GetNomeDatabase();
        var conexao = configurationManager.GetConexao();

        return $"{conexao}Database={nomeDatabase}";
    }
}
