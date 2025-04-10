using System.Globalization;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Exception.Finance
{
    public class CardCreditException : IValidationMessageResolver
    {
        private readonly Dictionary<string, string> _messages = new()
        {
            { "EX01", "Cartão Inválido" }
        };

        public string Resolve(string code, CultureInfo? culture = null)
        {
            return _messages.TryGetValue(code, out var message)
                ? message
                : $"Erro desconhecido: {code}";
        }
    }
}
