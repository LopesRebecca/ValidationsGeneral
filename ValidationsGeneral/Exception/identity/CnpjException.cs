using System.Globalization;
using ValidationsGeneral.Interface;

namespace ValidationsGeneral.Exception.identity
{
    class CnpjException : IValidationMessageResolver
    {
        private readonly Dictionary<string, string> _messages = new()
        {
            { "EX01", "O CNPJ está vazio." },
            { "EX02", "O CNPJ deve conter exatamente 11 dígitos." },
            { "EX03", "CNPJ inválido: todos os dígitos são iguais." },
            { "EX04", "CCNPJPF inválido: dígitos verificadores incorretos." },
        };

        public string Resolve(string code, CultureInfo? culture = null)
        {
            return _messages.TryGetValue(code, out var message)
                ? message
                : $"Erro desconhecido: {code}";
        }
    }
}
