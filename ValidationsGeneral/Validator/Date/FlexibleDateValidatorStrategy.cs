using System.Globalization;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Date
{
    public class FlexibleDateValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly CultureInfo _cultureInfo;
        private readonly string[] _dateFormats;

        public FlexibleDateValidatorStrategy(
            IValidationMessageResolver? messageResolver = null,
            string culture = "pt-BR",
            string[]? customFormats = null)
        {
            _messageResolver = messageResolver;
            _cultureInfo = new CultureInfo(culture);
            _dateFormats = customFormats ?? new[]
            {
                _cultureInfo.DateTimeFormat.ShortDatePattern,
                _cultureInfo.DateTimeFormat.LongDatePattern
            };
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
                return ValidationResult.Fail(FlexibleDateMsg.Code.EX01.ToString());

            bool isValid = DateTime.TryParseExact(
                input,
                _dateFormats,
                _cultureInfo,
                DateTimeStyles.None,
                out _
            );

            return isValid
                ? ValidationResult.Success()
                : ValidationResult.Fail(FlexibleDateMsg.Code.EX02.ToString());
        }
    }
}
