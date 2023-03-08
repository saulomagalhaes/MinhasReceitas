using MinhasReceitas.Application.Servicos.Token;

namespace UtilitarioTestes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "MlA2ZGZIRXV6ITJvJnlCYThTODk=");
    }
}
