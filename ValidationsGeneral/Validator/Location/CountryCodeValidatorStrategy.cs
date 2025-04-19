using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Location;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Location
{
    public class CountryCodeValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public CountryCodeValidatorStrategy(
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
                return ValidationResult.Fail(CountryCodeMsg.Code.EX01.ToString());

            if (!_validCountryCodes.Contains(input.ToUpperInvariant()))
                return ValidationResult.Fail(CountryCodeMsg.Code.EX02.ToString());

            return ValidationResult.Success();
        }

        private readonly HashSet<string> _validCountryCodes = 
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "BR", "US", "CA", "GB", "DE", "FR", "IT", "ES", "PT",
            "NL", "PL", "RU", "JP", "KR", "AU", "NZ", "IN", "MX",
            "AR", "ZA", "CN", "SE", "NO", "FI", "DK"
        };
    }
}
