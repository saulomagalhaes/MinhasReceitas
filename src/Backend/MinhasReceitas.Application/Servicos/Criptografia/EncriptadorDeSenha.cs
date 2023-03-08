using System.Security.Cryptography;
using System.Text;

namespace MinhasReceitas.Application.Servicos.Criptografia;

public class EncriptadorDeSenha
{
    private readonly string _chaveAdicional;

    public EncriptadorDeSenha(string chaveAdicional)
    {
        _chaveAdicional = chaveAdicional;
    }

    public string Criptografar(string senha)
    {
        var senhaComChaveAdicional = $"{senha}{_chaveAdicional}";

        var bytes = Encoding.UTF8.GetBytes(senhaComChaveAdicional);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);
        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] hashBytes)
    {
        var sb = new StringBuilder();
        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("X2"));
        }
        return sb.ToString();
    }
}
