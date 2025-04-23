using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this IValidatorStrategy validator, string input)
        {
            return validator.Validate(input).IsValid;
        }
    }
}