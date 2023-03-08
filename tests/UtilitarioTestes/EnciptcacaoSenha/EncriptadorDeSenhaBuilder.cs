using MinhasReceitas.Application.Servicos.Criptografia;

namespace UtilitarioTestes.EnciptcacaoSenha;

public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Instancia()
    {
        return new EncriptadorDeSenha("ABCD123");
    }
}
