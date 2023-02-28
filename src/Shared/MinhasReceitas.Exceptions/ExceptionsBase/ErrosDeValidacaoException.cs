namespace MinhasReceitas.Exceptions.ExceptionsBase;

public class ErrosDeValidacaoException : MinhasReceitasException
{
    public List<string> MensagensDeErro { get; set; }

    public ErrosDeValidacaoException(List<string> mensagensDeErro)
    {
        MensagensDeErro = mensagensDeErro;
    }
}
