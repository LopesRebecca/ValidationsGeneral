using System.Globalization;

namespace ValidationsGeneral.Interface
{
    public interface IValidationMessageResolver
    {
        string Resolve(string code, CultureInfo? culture = null);
    }
}
