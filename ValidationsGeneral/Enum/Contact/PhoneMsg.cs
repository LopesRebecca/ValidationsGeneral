namespace ValidationsGeneral.Enum.Contact
{
    public class PhoneMsg
    {
        public enum Code
        {
            EX01, // Telefone em branco
            EX02, // Telefone inválido
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Telefone em branco",
                Code.EX02 => "Telefone inválido",
                _ => "Erro desconhecido na validação de Telefone."
            };
        }
    }
}
