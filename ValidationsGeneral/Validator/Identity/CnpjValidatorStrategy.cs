using System.Text.RegularExpressions;
using ValidationsGeneral.Common;
using ValidationsGeneral.Validator.Identity;

namespace ValidationsGeneral.Validator.Identity
{
    public class CnpjValidatorStrategy : ValidatorBase
    {
        protected override ValidationResult ValidateInternal(string input, string erroMensage)
        {
            if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Fail("CNPJ em branco");

            // Remove caracteres não numéricos
            var cnpj = Regex.Replace(input, @"[^\d]", "");

            // Verifica se o CNPJ tem 14 dígitos
            if (cnpj.Length != 14) return ValidationResult.Fail("CNPJ com menos de 14 caracteres");

            // Verifica se todos os dígitos são iguais
            if (new string(cnpj[0], cnpj.Length) == cnpj) return ValidationResult.Fail("CNPJ inválido");

            // Calcula os dígitos verificadores
            bool isValid = ValidateCnpjDigits(cnpj);

            return isValid ? ValidationResult.Success() : ValidationResult.Fail("CNPJ inválido");
        }

        private bool ValidateCnpjDigits(string cnpj)
        {
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}