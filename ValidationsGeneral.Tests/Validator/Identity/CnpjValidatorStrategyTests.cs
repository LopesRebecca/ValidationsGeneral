using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Validator.Identity;

namespace ValidationsGeneral.Tests.Validator.Identity
{
    public class CnpjValidatorStrategyTests
    {
        private readonly CnpjValidatorStrategy _validator = new();

        [Theory]
        [InlineData("11.222.333/0001-81")]
        [InlineData("11222333000181")]
        [InlineData("04.252.011/0001-10")] // Natura
        [InlineData("40.688.134/0001-61")] // Google
        public void Should_Validate_Valid_Cnpjs(string cnpj)
        {
            var result = _validator.Validate(cnpj);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("11.222.333/0001-82", "EX04")] // Dígito inválido
        [InlineData("11222333000100", "EX04")]     // Dígito inválido
        [InlineData("00.000.000/0000-00", "EX03")] // Dígitos repetidos
        [InlineData("abcdefghijklll", "EX02")]     // Letras
        [InlineData("11.222.333/0001", "EX02")]    // Incompleto
        [InlineData("12345678", "EX02")]           // Muito curto
        public void Should_Invalidate_Invalid_Cnpjs(string cnpj, string code)
        {
            var result = _validator.Validate(cnpj);
            Assert.False(result.IsValid);
            Assert.Equal(code, result.Code); // Digito verificador inválido
        }

        [Fact]
        public void Should_Return_EX01_When_Input_Is_Null_Or_Empty()
        {
            Assert.Equal(CnpjCodeMsg.Code.EX01.ToString(), _validator.Validate(null!).Code);
            Assert.Equal(CnpjCodeMsg.Code.EX01.ToString(), _validator.Validate("").Code);
            Assert.Equal(CnpjCodeMsg.Code.EX01.ToString(), _validator.Validate("   ").Code);
        }

        [Fact]
        public void Should_Return_EX02_When_Length_Is_Invalid()
        {
            var result = _validator.Validate("12345678901");
            Assert.False(result.IsValid);
            Assert.Equal(CnpjCodeMsg.Code.EX02.ToString(), result.Code);
        }

        [Fact]
        public void Should_Return_EX03_When_All_Digits_Are_The_Same()
        {
            var result = _validator.Validate("11111111111111");
            Assert.False(result.IsValid);
            Assert.Equal(CnpjCodeMsg.Code.EX03.ToString(), result.Code);
        }
    }
}
