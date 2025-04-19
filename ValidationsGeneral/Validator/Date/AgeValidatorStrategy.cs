using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Contact
{
    public class AgeValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly int _minAge;
        private readonly int _maxAge;

        public AgeValidatorStrategy(
            IValidationMessageResolver? messageResolver = null,
            int minAge = 18,
            int maxAge = 65)
        {
            _messageResolver = messageResolver;
            _minAge = minAge;
            _maxAge = maxAge;
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
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int age))
                return ValidationResult.Fail(AgeMsg.Code.EX01.ToString());

            if (age < _minAge || age > _maxAge)
                return ValidationResult.Fail(AgeMsg.Code.EX02.ToString());

            return ValidationResult.Success();
        }
    }
}
