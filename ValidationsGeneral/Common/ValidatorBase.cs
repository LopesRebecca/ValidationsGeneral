using ValidationsGeneral.Enum;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Common
{
    public abstract class ValidatorBase : IValidatorStrategy
    {
        public ValidationResult Validate(string input, string menssage)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Fail(menssage is null ? "Erro Generico": menssage);

            return ValidateInternal(input, menssage);
        }

        protected abstract ValidationResult ValidateInternal(string input, string menssage);
    }
}
