using FluentValidation;
using MinhasReceitas.Communication.Requisicoes;
using MinhasReceitas.Exceptions;
using System.Text.RegularExpressions;

namespace MinhasReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioValidator : AbstractValidator<RequisicaoRegistrarUsuarioJson>
{
	public RegistrarUsuarioValidator()
	{
		RuleFor(u => u.Nome).NotEmpty().WithMessage(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO);

		RuleFor(u => u.Email).NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO);
		When(u => !string.IsNullOrWhiteSpace(u.Email), () =>
		{
			RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO);
		});

		RuleFor(u => u.Senha).NotEmpty().WithMessage(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO);
		When(u => !string.IsNullOrWhiteSpace(u.Senha), () =>
		{
			RuleFor(u => u.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES);
		});

		RuleFor(u => u.Telefone).NotEmpty().WithMessage(ResourceMensagensDeErro.TELEFONE_USUARIO_EM_BRANCO);
		When(u => !string.IsNullOrEmpty(u.Telefone), () =>
		{
			RuleFor(u => u.Telefone).Custom((telefone, contexto) =>
			{
				string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
				var isMatch = Regex.IsMatch(telefone, padraoTelefone);
				if (!isMatch)
				{
					contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
				}
			});
		});
	}
}
