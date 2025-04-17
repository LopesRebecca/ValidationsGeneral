namespace ValidationsGeneral.Enum.Identity
{
    public class CardCreditMsg
    {
        public enum Code
        {
            EX01, // Cartão invalido
            EX02, // Cartão em branco
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Cartão inválido",
                Code.EX02 => "Cartão em branco",
                _ => "Erro desconhecido na validação de Cartão."
            };
        }
    }
}
