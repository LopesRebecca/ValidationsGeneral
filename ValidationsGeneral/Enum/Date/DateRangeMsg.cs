using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationsGeneral.Enum.Date
{
    public static class DateRangeMsg
    {
        public enum Code
        {
            EX01, // Input nulo ou vazio
            EX02, // Data inválida para a cultura
            EX03, // Data abaixo do mínimo
            EX04  // Data acima do máximo
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Entrada vazia ou nula",
                Code.EX02 => "Formato inválido para a cultura informada",
                Code.EX03 => "Data abaixo do mínimo permitido",
                Code.EX04 => "Data acima do máximo permitido",
                _ => "Erro desconhecido na validação de periódo de Data"
            };
        }
    }
}
