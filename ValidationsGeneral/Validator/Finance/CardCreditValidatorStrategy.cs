using ValidationsGeneral.Common;
using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Validator.Finance
{
    public class CardCreditValidatorStrategy : ValidatorBase
    {
        private readonly IValidationMessageResolver? _messageResolver;

        public CardCreditValidatorStrategy(IValidationMessageResolver? messageResolver = null)
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
                return Fail(CardCreditMsg.Code.EX02.ToString());

            int sum = 0;
            bool doubleDigit = false;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(input[i])) continue;

                int digit = input[i] - '0';

                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            bool isValid = sum % 10 == 0;
            
            return isValid 
                ? ValidationResult.Success() 
                : ValidationResult.Fail(CardCreditMsg.Code.EX01.ToString());
        }
    }
}
