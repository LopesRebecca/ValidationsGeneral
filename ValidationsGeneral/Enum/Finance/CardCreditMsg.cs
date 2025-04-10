namespace ValidationsGeneral.Enum.Identity
{
    public class CardCreditMsg
    {
        public enum Code
        {
            EX01, // Cartão invalido
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Cartão inválido",
                _ => "Erro desconhecido na validação de Cartão."
            };
        }
    }
}
