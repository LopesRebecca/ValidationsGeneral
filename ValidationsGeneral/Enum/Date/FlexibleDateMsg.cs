namespace ValidationsGeneral.Enum.Date
{
    public static class FlexibleDateMsg
    {
        public enum Code
        {
            EX01, // Input vazio ou nulo
            EX02  // Formato inválido para a cultura informada
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Entrada vazia ou nula",
                Code.EX02 => "Formato inválido para a cultura informada",
                _ => "Erro desconhecido na validação na formatação de Data"
            };
        }
    }
}
