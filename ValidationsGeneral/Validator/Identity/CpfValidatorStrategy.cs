using System.Text.RegularExpressions;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Identity
{
    public class CpfValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public CpfValidatorStrategy(IValidationMessageResolver? messageResolver = null)
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
                return ValidationResult.Fail(CpfCodeMsg.Code.EX01.ToString());

            var cpf = Regex.Replace(input, @"[^\d]", "");

            if (cpf.Length != 11) 
                return ValidationResult.Fail(CpfCodeMsg.Code.EX02.ToString());

            if (new string(cpf[0], cpf.Length) == cpf) 
                return ValidationResult.Fail(CpfCodeMsg.Code.EX03.ToString());

            bool isValid = ValidateCpfDigits(cpf);

            return  isValid 
                ? ValidationResult.Success() 
                : ValidationResult.Fail(CpfCodeMsg.Code.EX04.ToString());
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
