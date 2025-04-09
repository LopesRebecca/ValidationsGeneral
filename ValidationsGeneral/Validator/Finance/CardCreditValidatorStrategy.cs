using ValidationsGeneral.Common;

namespace ValidationsGeneral.Validator.Finance
{
    public class CardCreditValidatorStrategy : ValidatorBase
    {
        protected override ValidationResult ValidateInternal(string input)
        {
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
            
            return isValid ? ValidationResult.Success() : ValidationResult.Fail("Cartão Inválido");
        }
    }
}
