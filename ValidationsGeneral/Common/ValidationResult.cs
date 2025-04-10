using System.Globalization;

namespace ValidationsGeneral.Common
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string? Code { get; }
        public string? Message { get; }

        private ValidationResult(bool isValid, string? code = null, string? message = null)
        {
            IsValid = isValid;
            Code = code;
            Message = message;
        }

        public static ValidationResult Success() => new(true);

        public static ValidationResult Fail(string code, string? message = null) =>
            new(false, code, message);
    }
}
