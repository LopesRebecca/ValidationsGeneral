using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationsGeneral.Enum.Contact
{
    public class IpAddressMsg
    {
        public enum Code
        {
            EX01, // Endereço de IP em branco
            EX02, // Endereço de IP inválido
        }

        public static string GetMessage(Code code)
        {
            return code switch
            {
                Code.EX01 => "Endereço de IP em branco",
                Code.EX02 => "Endereço de IP inválido",
                _ => "Erro desconhecido na validação de Endereço de IP."
            };
        }
    }
}
