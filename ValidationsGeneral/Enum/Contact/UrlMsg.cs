namespace ValidationsGeneral.Enum.Contact
{
    public class UrlMsg
    {
        public enum Code
        {
            EX01, // URL em branco
            EX02, // URL com formato inválido,
            EX03, // Regra customizada falhou
            EX04, //Esquema inválido
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "URL em branco",
                Code.EX02 => "URL inválido",
                Code.EX03 => "URL inválida, não atende a regra customizada",
                Code.EX04 => "Esquema inválido, deve ser está no esquema",
                _ => "Erro desconhecido na validação de URL."
            };
        }
    }
}
