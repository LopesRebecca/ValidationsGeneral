using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Exception.identity;
using ValidationsGeneral.Validator.Identity;

namespace ValidationsGeneral.Tests.Validator.Identity
{
    public class CpfValidatorStrategyTests
    {
        private readonly CpfValidatorStrategy _defaultValidator = new();
        private readonly CpfValidatorStrategy _withResolver;

        public CpfValidatorStrategyTests()
        {
            _withResolver = new CpfValidatorStrategy(new CpfException());
        }

        [Theory]
        [InlineData("111.444.777-35", true)]
        [InlineData("000.000.000-00", false)]
        [InlineData("99999999999", false)]
        [InlineData("", false)]
        public void Validate_WithDefaultValidator_ShouldReturnCorrectResult(string cpf, bool expectedValid)
        {
            var result = _defaultValidator.Validate(cpf);
            Assert.Equal(expectedValid, result.IsValid);
        }

        [Fact]
        public void Validate_WithCustomMessage_ShouldOverride()
        {
            var customMessage = "CPF inválido customizado";
            var result = _defaultValidator.Validate("12345678900", customMessage);

            Assert.False(result.IsValid);
            Assert.Equal(CpfCodeMsg.Code.EX04.ToString(), result.Code);
            Assert.Equal(customMessage, result.Message);
        }

        [Fact]
        public void Validate_WithResolver_ShouldProvideCustomMessage()
        {
            var result = _withResolver.Validate("12345678900");

            Assert.False(result.IsValid);
            Assert.Equal(CpfCodeMsg.Code.EX04.ToString(), result.Code);
            Assert.Equal("CPF inválido: dígitos verificadores incorretos.", result.Message);
        }
    }
}
