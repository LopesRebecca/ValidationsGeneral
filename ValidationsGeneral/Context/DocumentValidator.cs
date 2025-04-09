using ValidationsGeneral.Common;
using ValidationsGeneral.Extensions;
using ValidationsGeneral.Factory;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Context
{
    public class DocumentValidator
    {
        private readonly IValidatorStrategy? _strategy;

        public DocumentValidator(DocumentType type)
        {
            _strategy = ValidatorFactory.Create(type);
        }

        public DocumentValidator(IValidatorStrategy strategy)
        {
            _strategy = strategy;
        }

        public bool Validation(string input)
        {
            if (_strategy == null)
                return false;

            return _strategy.IsValid(input);
        }

        public ValidationResult ValidarComResultado(string input)
        {
            if (_strategy == null)
                return ValidationResult.Fail("Validador não definido.");

            bool valido = _strategy.IsValid(input);

            return valido
                ? ValidationResult.Success()
                : ValidationResult.Fail("Valor inválido para o tipo informado.");
        }
    }
}