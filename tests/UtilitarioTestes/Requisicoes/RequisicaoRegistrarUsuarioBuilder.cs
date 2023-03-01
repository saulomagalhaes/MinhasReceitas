using Bogus;
using MinhasReceitas.Communication.Requisicoes;

namespace UtilitarioTestes.Requisicoes;

public class RequisicaoRegistrarUsuarioBuilder
{
    public static RequisicaoRegistrarUsuarioJson Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequisicaoRegistrarUsuarioJson>()
            .RuleFor(u => u.Nome, b => b.Person.FullName)
            .RuleFor(u => u.Email, b => b.Internet.Email())
            .RuleFor(u => u.Senha, b => b.Internet.Password(tamanhoSenha))
            .RuleFor(u => u.Telefone, b => b.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{b.Random.Int(min:1, max:9)}"));
    }
}
