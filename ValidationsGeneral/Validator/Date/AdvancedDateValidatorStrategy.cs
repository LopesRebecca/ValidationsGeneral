using System.Globalization;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Date
{
    public class AdvancedDateValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly string[] _formats;
        private readonly CultureInfo _culture;
        private readonly DateTime? _minDate;
        private readonly DateTime? _maxDate;

        public AdvancedDateValidatorStrategy(
            IValidationMessageResolver? messageResolver = null,
            string culture = "pt-BR",
            string[]? formats = null,
            DateTime? minDate = null,
            DateTime? maxDate = null)
        {
            _messageResolver = messageResolver;
            _culture = new CultureInfo(culture);
            _formats = formats ?? new[]
            {
                _culture.DateTimeFormat.ShortDatePattern,
                _culture.DateTimeFormat.LongDatePattern
            };
            _minDate = minDate;
            _maxDate = maxDate;
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
                return ValidationResult.Fail(AdvancedDateMsg.Code.EX01.ToString());

            if (!DateTime.TryParseExact(
                input,
                _formats,
                _culture,
                DateTimeStyles.None,
                out var parsedDate))
                return ValidationResult.Fail(AdvancedDateMsg.Code.EX02.ToString());

            if (_minDate.HasValue && parsedDate < _minDate.Value)
                return ValidationResult.Fail(AdvancedDateMsg.Code.EX03.ToString());

            if (_maxDate.HasValue && parsedDate > _maxDate.Value)
                return ValidationResult.Fail(AdvancedDateMsg.Code.EX04.ToString());

            return ValidationResult.Success();
        }
    }
}
