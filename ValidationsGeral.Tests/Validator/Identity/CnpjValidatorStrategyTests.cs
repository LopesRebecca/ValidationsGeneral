using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Exception.identity;
using ValidationsGeneral.Validator.Identity;

namespace ValidationsGeral.Tests.Validator.Identity
{
    [TestFixture]
    public class CnpjValidatorStrategyTests
    {
        private readonly CnpjValidatorStrategy _strategy;

        public CnpjValidatorStrategyTests()
        {
            _strategy = new CnpjValidatorStrategy(new CnpjException());
        }

        #region InvalidCNPJ
        [TestCase("", "EX01")]
        [TestCase(" ", "EX01")]
        [TestCase("", "CNPJ em branco.")]
        [TestCase(" ", "CNPJ em branco.")]
        public void Validate_EmpityCpf_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CnpjCodeMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("80.567.731/0001-5", "EX02")]
        [TestCase("876008620001336", "CPF com o tamanho inválido")]
        [TestCase("123", "O CPF deve conter exatamente 11 dígitos.")]
        [TestCase("testePalavras", "EX02")]
        public void Validate_InvalidLegthCpf_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CnpjCodeMsg.Code.EX02.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("77777777777777", "EX03")]
        [TestCase("88.888.888/8888-88", "CPF com números repitidos, verifique")]
        [TestCase("88.888.888/8888-88", "EX03")]
        public void Validate_RepetDigit_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CnpjCodeMsg.Code.EX03.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("70191515000103", "EX04")]
        [TestCase("58.670.806/0001-79", "CNPJ inválido, verifique")]
        public void Validate_InvalidCPF_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CnpjCodeMsg.Code.EX04.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }
        #endregion

        #region ValidCNPJ
        [TestCase("78032473000168")]
        [TestCase("54.951.766/0001-80")]
        public void Validate_ValidCPF_ReturnsInvalid(string input)
        {
            // Act
            var result = _strategy!.Validate(input);

            // Assert
            Assert.IsTrue(result.IsValid);
        }
        #endregion
    }
}