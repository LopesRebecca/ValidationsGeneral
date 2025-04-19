using System;
using System.Text.RegularExpressions;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Contact
{
    public class EmailValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly Func<string, bool>? _customRule;

        public EmailValidatorStrategy(IValidationMessageResolver? messageResolver = null, Func<string, bool>? customRule = null)
        {
            _messageResolver = messageResolver;
            _customRule = customRule;
        }

        public ValidationResult Validate(string input, string? customMessage = null)
        {
            var result = ValidateInternal(input);

            // Aplicando a regra customizada
            if (_customRule != null && !_customRule(input))
            {
                return ValidationResult.Fail(EmailMsg.Code.EX03.ToString(), customMessage ?? "A regra customizada falhou.");
            }

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
                return ValidationResult.Fail(EmailMsg.Code.EX01.ToString());

            bool isValid = ValidateEmailFormat(input);

            return isValid
                ? ValidationResult.Success()
                : ValidationResult.Fail(EmailMsg.Code.EX02.ToString());
        }

        private bool ValidateEmailFormat(string input)
        {
            Regex emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
            return emailRegex.IsMatch(input);
        }
    }
}