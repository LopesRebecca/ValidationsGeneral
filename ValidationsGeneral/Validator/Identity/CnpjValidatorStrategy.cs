using System.Text.RegularExpressions;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Identity
{
    public class CnpjValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public CnpjValidatorStrategy(IValidationMessageResolver? messageResolver = null)
        {
            _messageResolver = messageResolver;
        }

        public ValidationResult Validate(string input, string? customMessage = null)
        {
            var result = ValidateInternal(input);

            if (!result.IsValid)
            {
                var resolvedMessage = customMessage
                    ?? _messageResolver?.Resolve(result.Code!)
                    ?? null;

                return ValidationResult.Fail(result.Code!, resolvedMessage);
            }

            return result;
        }

        protected override ValidationResult ValidateInternal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) 
                return ValidationResult.Fail(CnpjCodeMsg.Code.EX01.ToString());

            var cnpj = Regex.Replace(input, @"[^\d]", "");

            if (cnpj.Length != 14) 
                return ValidationResult.Fail(CnpjCodeMsg.Code.EX02.ToString());

            if (new string(cnpj[0], cnpj.Length) == cnpj) 
                return ValidationResult.Fail(CnpjCodeMsg.Code.EX03.ToString());

            bool isValid = ValidateCnpjDigits(cnpj);

            return isValid 
                ? ValidationResult.Success() 
                : ValidationResult.Fail(CnpjCodeMsg.Code.EX04.ToString());
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