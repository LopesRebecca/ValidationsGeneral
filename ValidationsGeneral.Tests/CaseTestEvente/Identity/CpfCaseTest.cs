using NUnit.Framework;

namespace ValidationsGeneral.Tests.CaseTestEvente.Identity
{
    public class CpfCaseTest
    {
        public static List<TestCaseData> InvalidCPF
        {
            get
            {
                return new List<TestCaseData>
                {
                     new TestCaseData(
                         string.Empty,
                         "CPF em branco."),

                     new TestCaseData(
                         null,
                         "CPF em branco."),

                     new TestCaseData(
                         "1234567891",
                         "CPF deve conter 11 dígitos."),

                     new TestCaseData(
                         "11111111111",
                         "CPF inválido (dígitos repetidos)."),

                     new TestCaseData(
                         "12345698751",
                         "CPF inválido (dígitos verificadores incorretos)."),

                      new TestCaseData(
                         string.Empty,
                         "EX01"),

                     new TestCaseData(
                         null,
                         "EX01"),

                     new TestCaseData(
                         "1234567891",
                         "EX02"),

                     new TestCaseData(
                         "11111111111",
                         "EX03"),

                     new TestCaseData(
                         "12345698751",
                         "EX04"),
                };
            }
        }

        public static List<TestCaseData> ValidCPF
        {
            get
            {
                return new List<TestCaseData>
                {
                     new TestCaseData("08575725394"),
                     new TestCaseData("085.757.253-94"),
                     new TestCaseData("085. 757. 253-9 4"),
                };
            }
        }
    }
}
