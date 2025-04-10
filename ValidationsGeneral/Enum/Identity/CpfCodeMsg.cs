namespace ValidationsGeneral.Enum.Identity
{
    public class CpfCodeMsg
    {
        public enum Code
        {
            EX01, // CPF em branco
            EX02, // CPF com menos de 11 caracteres
            EX03, // CPF com dígitos repetidos
            EX04  // Dígitos verificadores inválidos
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "CPF em branco.",
                Code.EX02 => "CPF deve conter 11 dígitos.",
                Code.EX03 => "CPF inválido (dígitos repetidos).",
                Code.EX04 => "CPF inválido (dígitos verificadores incorretos).",
                _ => "Erro desconhecido na validação de CPF."
            };
        }
    }
}
