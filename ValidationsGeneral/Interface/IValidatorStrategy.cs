using ValidationsGeneral.Common;

namespace ValidationsGeneral.Interface
{
    public interface IValidatorStrategy
    {
        ValidationResult Validate(string input, string erroMensage);
    }
}
