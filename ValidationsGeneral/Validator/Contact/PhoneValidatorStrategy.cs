using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Contact
{
    public class PhoneValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public PhoneValidatorStrategy(
            IValidationMessageResolver? messageResolver = null)
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
                return ValidationResult.Fail(PhoneMsg.Code.EX01.ToString());

            bool isValid = ValidateCpfDigits(input);

            return isValid
                ? ValidationResult.Success()
                : ValidationResult.Fail(PhoneMsg.Code.EX02.ToString());
        }


        private bool ValidateCpfDigits(string input)
        {
            Regex PhoneRegex = new(@"^\+?\d{10,15}$", RegexOptions.Compiled);
            return PhoneRegex.IsMatch(input);
        }
    }
}
