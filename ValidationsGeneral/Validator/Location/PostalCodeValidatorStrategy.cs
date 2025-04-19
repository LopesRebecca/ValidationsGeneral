using System.Text.RegularExpressions;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Enum.Location;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Location
{
    public class PostalCodeValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly string?
            _countryCode = "BR";

        public PostalCodeValidatorStrategy(
            IValidationMessageResolver? messageResolver = null,
            string? culture= "BR")
        {
            _messageResolver = messageResolver;
            _countryCode = culture?.ToUpperInvariant();
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
                return ValidationResult.Fail(PostalCodeMsg.Code.EX01.ToString());

            if (!_postalCodeRegex.TryGetValue(_countryCode!, out var pattern))
                return ValidationResult.Fail(PostalCodeMsg.Code.EX02.ToString()); // Código de país não suportado

            if (!Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                return ValidationResult.Fail(PostalCodeMsg.Code.EX03.ToString()); // Código postal inválido

            return ValidationResult.Success();
        }

        private readonly Dictionary<string, string> _postalCodeRegex = new()
        {
            { "BR", @"^\d{5}-\d{3}$" },
            { "US", @"^\d{5}(-\d{4})?$" },
            { "CA", @"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d$" },
            { "GB", @"^([A-Z]{1,2}\d{1,2}[A-Z]?) ?\d[A-Z]{2}$" },
            { "DE", @"^\d{5}$" },
            { "FR", @"^\d{5}$" },
            { "IT", @"^\d{5}$" },
            { "ES", @"^\d{5}$" },
            { "PT", @"^\d{4}-\d{3}$" },
            { "NL", @"^\d{4}\s?[A-Z]{2}$" },
            { "PL", @"^\d{2}-\d{3}$" },
            { "RU", @"^\d{6}$" },
            { "JP", @"^\d{3}-\d{4}$" },
            { "KR", @"^\d{5}$" },
            { "AU", @"^\d{4}$" },
            { "NZ", @"^\d{4}$" },
            { "IN", @"^\d{6}$" },
            { "MX", @"^\d{5}$" },
            { "AR", @"^[A-Z]\d{4}[A-Z]{3}$" },
            { "ZA", @"^\d{4}$" }
        };
    }
}
