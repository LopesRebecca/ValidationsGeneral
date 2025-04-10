namespace ValidationsGeneral.Enum.Identity
{
    public class CnpjCodeMsg
    {
        public enum Code
        {
            EX01, // CNPJ em branco
            EX02, // CNPJ com menos de 11 caracteres
            EX03, // CNPJ com dígitos repetidos
            EX04  // Dígitos verificadores inválidos
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "CNPJ em branco.",
                Code.EX02 => "CNPJ deve conter 11 dígitos.",
                Code.EX03 => "CNPJ inválido (dígitos repetidos).",
                Code.EX04 => "CNPJ inválido (dígitos verificadores incorretos).",
                _ => "Erro desconhecido na validação de CNPJ."
            };
        }
    }
}
