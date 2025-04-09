using System.Globalization;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Exception.identity
{
    public class CpfException : IValidationMessageResolver
    {
        private readonly Dictionary<string, string> _messages = new()
        {
            { "EX01", "O CPF está vazio." },
            { "EX02", "O CPF deve conter exatamente 11 dígitos." },
            { "EX03", "CPF inválido: todos os dígitos são iguais." },
            { "EX04", "CPF inválido: dígitos verificadores incorretos." },
        };

        public string Resolve(string code, CultureInfo? culture = null)
        {
            return _messages.TryGetValue(code, out var message)
                ? message
                : $"Erro desconhecido: {code}";
        }
    }
}
