namespace ValidationsGeneral.Enum.Contact
{
    public class EmailMsg
    {
        public enum Code
        {
            EX01, // Email em branco
            EX02, // Email inválido.
            EX03, // Regra customizada falhou
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Email em branco",
                Code.EX02 => "Email inválido",
                Code.EX03 => "Email inválido, não atende a regra customizada",
                _ => "Erro desconhecido na validação de Email."
            };
        }
    }
}
