using System.Text.RegularExpressions;
using ValidationsGeneral.Common;

namespace ValidationsGeneral.Validator.Identity
{
    public class CpfValidatorStrategy : ValidatorBase
    {
        protected override ValidationResult ValidateInternal(string input, string message)
        {
            if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Fail("CPF em branco");

            // Remove caracteres não numéricos
            var cpf = Regex.Replace(input, @"[^\d]", "");

            // Verifica se o CPF tem 11 dígitos
            if (cpf.Length != 11) return ValidationResult.Fail("CPF com menos de 11 caracteres");

            // Verifica se todos os dígitos são iguais
            if (new string(cpf[0], cpf.Length) == cpf) return ValidationResult.Fail("CPF Inválido");

            // Calcula os dígitos verificadores
            bool isValid = ValidateCpfDigits(cpf);

            return  isValid ? ValidationResult.Success() : ValidationResult.Fail(message);
        }

        
        private bool ValidateCpfDigits(string cpf)
        {
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}
