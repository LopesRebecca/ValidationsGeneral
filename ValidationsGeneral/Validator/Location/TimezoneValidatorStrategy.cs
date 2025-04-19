using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Location;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Location
{
    public class TimezoneValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly string? _timezoneFormat = "UTC";

        public TimezoneValidatorStrategy(
            IValidationMessageResolver? messageResolver = null, 
            string? timezoneFormat = null)
        {
            _messageResolver = messageResolver;
            _timezoneFormat = timezoneFormat;
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
                return ValidationResult.Fail(TimezoneMsg.Code.EX01.ToString());

            if (!_validTimezones.Contains(input))
                return ValidationResult.Fail(TimezoneMsg.Code.EX02.ToString());

            return ValidationResult.Success();
        }

        private readonly HashSet<string> _validTimezones = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "UTC",
            "America/Sao_Paulo",
            "America/New_York",
            "Europe/Lisbon",
            "Europe/Berlin",
            "Asia/Tokyo",
            "Asia/Shanghai",
            "Australia/Sydney",
            "Africa/Johannesburg",
            "Pacific/Auckland"
        };
    }
}