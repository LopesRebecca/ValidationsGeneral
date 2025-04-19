namespace ValidationsGeneral.Enum.Date
{
    public static class IsoDateMsg
    {
        public enum Code
        {
            EX01, // Entrada vazia ou nula
            EX02  // Formato inválido (não está em yyyy-MM-dd)
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Entrada vazia ou nula",
                Code.EX02 => "Formato inválido (não está em yyyy-MM-dd)",
                _ => "Erro desconhecido na validação de Data de acordo com a ISO."
            };
        }
    }
}