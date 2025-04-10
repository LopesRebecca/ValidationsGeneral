using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Common
{
    public abstract class ValidatorBase : IValidatorStrategy
    {
        public ValidationResult Validate(string input)
        {
            return ValidateInternal(input);
        }

        protected abstract ValidationResult ValidateInternal(string input);

        protected ValidationResult Fail(string code, string? message = null)
        {
            return ValidationResult.Fail(code, message);
        }
    }
}
