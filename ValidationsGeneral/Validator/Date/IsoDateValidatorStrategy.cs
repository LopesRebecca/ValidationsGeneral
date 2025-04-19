using System.Globalization;
using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Contact
{
    public class IsoDateValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public IsoDateValidatorStrategy(IValidationMessageResolver? messageResolver = null)
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
                return ValidationResult.Fail(IsoDateMsg.Code.EX01.ToString());

            bool isValid = DateTime.TryParseExact(
                input,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );

            return isValid
                ? ValidationResult.Success()
                : ValidationResult.Fail(IsoDateMsg.Code.EX02.ToString());
        }
    }
}