using DomainLabBackend.Domain;
using FluentValidation;
using System.Text.RegularExpressions;


namespace DomainLabBackend.Validation
{
    public class Validation
    {
        public class EmpresaValidator : AbstractValidator<EmpresaDto>
        {
            public EmpresaValidator()
            {
                RuleFor(cnpj => cnpj.Cnpj)
                    .NotEmpty().WithMessage("O CNPJ não pode estar vazio.")
                    .Length(14).WithMessage("O CNPJ deve ter 14 dígitos.")
                    .Must(ValidateCnpj).WithMessage("CNPJ inválido.");

                RuleFor(cnpj => cnpj.RazaoSocial)
                    .NotEmpty().WithMessage("O CNPJ não pode estar vazio.")
                    .Length(14).WithMessage("O CNPJ deve ter 14 dígitos.")
                    .Must(ValidateCnpj).WithMessage("CNPJ inválido.");
            }


            private bool ValidateCnpj(string cnpj)
            {
                cnpj = Regex.Replace(cnpj, @"[^\d]", "");

                if (cnpj.Length != 14)
                    return false;

                int soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(cnpj[i].ToString()) * (5 - i);
                int resto = soma % 11;
                int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(cnpj[i].ToString()) * (6 - i);
                resto = soma % 11;
                int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

                return int.Parse(cnpj[12].ToString()) == digitoVerificador1 && int.Parse(cnpj[13].ToString()) == digitoVerificador2;
            }
        }
    }
}
