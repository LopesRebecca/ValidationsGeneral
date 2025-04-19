using System.Globalization;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Date
{
    public class DateRangeValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly DateTime? _minDate;
        private readonly DateTime? _maxDate;
        private readonly CultureInfo _culture;

        public DateRangeValidatorStrategy(
            IValidationMessageResolver? messageResolver = null,
            string culture = "pt-BR",
            DateTime? minDate = null,
            DateTime? maxDate = null)
        {
            _messageResolver = messageResolver;
            _culture = new CultureInfo(culture);
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
                return ValidationResult.Fail(DateRangeMsg.Code.EX01.ToString());

            if (!DateTime.TryParse(
                input,
                _culture,
                DateTimeStyles.None,
                out var parsedDate))
                return ValidationResult.Fail(DateRangeMsg.Code.EX02.ToString());

            if (_minDate.HasValue && parsedDate < _minDate.Value)
                return ValidationResult.Fail(DateRangeMsg.Code.EX03.ToString());

            if (_maxDate.HasValue && parsedDate > _maxDate.Value)
                return ValidationResult.Fail(DateRangeMsg.Code.EX04.ToString());

            return ValidationResult.Success();
        }
    }
}
