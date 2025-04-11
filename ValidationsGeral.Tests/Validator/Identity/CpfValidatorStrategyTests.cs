using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Exception.identity;
using ValidationsGeneral.Validator.Identity;

namespace ValidationsGeral.Tests.Validator.Identity
{
    [TestFixture]
    public class CpfValidatorStrategyTests
    {
        private readonly CpfValidatorStrategy _strategy;

        public CpfValidatorStrategyTests()
        {
            _strategy = new CpfValidatorStrategy(new CpfException());
        }

        #region InvalidCPF
        [TestCase("", "EX01")]
        [TestCase(" ", "EX01")]
        [TestCase("", "CPF em branco.")]
        [TestCase(" ", "CPF em branco.")]
        public void Validate_EmpityCpf_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CpfCodeMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("0857572534", "EX02")]
        [TestCase("085.757.253/94011-023", "CPF com o tamanho inválido")]
        [TestCase("123", "O CPF deve conter exatamente 11 dígitos.")]
        [TestCase("testePalavras", "EX02")]
        public void Validate_InvalidLegthCpf_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CpfCodeMsg.Code.EX02.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("11111111111", "EX03")]
        [TestCase("111.111.111-11", "CPF com números repitidos, verifique")]
        [TestCase("111.111.111-11", "EX03")]
        public void Validate_RepetDigit_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CpfCodeMsg.Code.EX03.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("78965415632", "EX04")]
        [TestCase("045.789.120-65", "CPF inválido, verifique")]
        public void Validate_InvalidCPF_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CpfCodeMsg.Code.EX04.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }
        #endregion

        #region ValidCPF
        [TestCase("08575725394")]
        [TestCase("085.757.253-94")]
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