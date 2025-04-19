using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validators.Strategies
{
    public class UrlValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;
        private readonly IEnumerable<string>? _allowedSchemes;
        private readonly Func<Uri, bool>? _customRule;

        public UrlValidatorStrategy(
            IValidationMessageResolver? messageResolver = null,
            IEnumerable<string>? allowedSchemes = null,
            Func<Uri, bool>? customRule = null)
        {
            _messageResolver = messageResolver;
            _allowedSchemes = allowedSchemes;
            _customRule = customRule;
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
                return ValidationResult.Fail(UrlMsg.Code.EX01.ToString());

            if (!Uri.TryCreate(input, UriKind.Absolute, out var uri))
                return ValidationResult.Fail(UrlMsg.Code.EX02.ToString());

            // Se tiver regra customizada, aplica
            if (_customRule != null && !_customRule(uri))
                return ValidationResult.Fail(UrlMsg.Code.EX03.ToString());

            // Caso contrário, valida por esquema
            var schemes = _allowedSchemes?.Select(s => s.ToLower()) ?? new[] { "http", "https" };

            if (!schemes.Contains(uri.Scheme.ToLower()))
                return ValidationResult.Fail(UrlMsg.Code.EX04.ToString());

            return ValidationResult.Success();
        }
    }
}
