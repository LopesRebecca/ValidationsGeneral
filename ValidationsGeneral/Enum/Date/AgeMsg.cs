namespace ValidationsGeneral.Enum.Date
{
    public static class AgeMsg
    {
        public enum Code
        {
            EX01, // Entrada inválida ou não numérica
            EX02  // Idade fora da faixa permitida
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Entrada inválida ou não numérica",
                Code.EX02 => "Idade fora da faixa permitida",
                _ => "Erro desconhecido na validação de Idade."
            };
        }
    }
}