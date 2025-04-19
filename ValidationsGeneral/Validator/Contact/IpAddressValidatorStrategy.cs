using System.Net;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Contact
{
    public class IpAddressValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public IpAddressValidatorStrategy(IValidationMessageResolver? messageResolver = null)
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
                return ValidationResult.Fail(IpAddressMsg.Code.EX01.ToString());

            bool isValid = ValidateCpfDigits(input);

            return isValid
                ? ValidationResult.Success()
                : ValidationResult.Fail(IpAddressMsg.Code.EX02.ToString());
        }


        private bool ValidateCpfDigits(string input)
        {
            return !string.IsNullOrWhiteSpace(input) &&
                   IPAddress.TryParse(input, out _);
        }
    }
}
