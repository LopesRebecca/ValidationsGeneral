namespace ValidationsGeneral.Enum.Location
{
    public class CountryCodeMsg
    {
        public enum Code
        {
            EX01, // CEP em branco
            EX02, // País não implementado ainda
            EX03, // CEP inválido 
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "CEP em branco",
                Code.EX02 => "País não implementado ainda",
                Code.EX03 => "CEP inválido",
                _ => "Erro desconhecido na validação de CEP."
            };
        }
    }
}
