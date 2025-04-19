namespace ValidationsGeneral.Enum.Location
{
    public class TimezoneMsg
    {
        public enum Code
        {
            EX01, // TimeZone em branco
            EX02, // Timezone inválido
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "TimeZone em branco",
                Code.EX02 => "Timezone inválido",
                _ => "Erro desconhecido na validação de TimeZone."
            };
        }
    }
}
