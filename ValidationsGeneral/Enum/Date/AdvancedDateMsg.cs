namespace ValidationsGeneral.Enum.Date
{
    public class AdvancedDateMsg
    {
        public enum Code
        {
            EX01, // Input vazio
            EX02, // Data com formato inválido
            EX03, // Abaixo do mínimo
            EX04  // Acima do máximo
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Entrada inválida ou não numérica",
                Code.EX02 => "Data com formato inválido",
                Code.EX03 => "Data abaixo do mínimo permitido",
                Code.EX04 => "Data acima do máximo permitido",
                _ => "Erro desconhecido na validação de data."
            };
        }
    }
}
