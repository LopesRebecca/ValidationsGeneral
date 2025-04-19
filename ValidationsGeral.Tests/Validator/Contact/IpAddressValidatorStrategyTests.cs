using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Validator.Contact;

namespace ValidationsGeral.Tests.Validator.Contact
{
    [TestFixture]
    public class IpAddressValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsNullOrEmpty(string? input)
        {
            // Arrange
            var validator = new IpAddressValidatorStrategy();

            // Act
            var result = validator.Validate(input, "Mensagem customizada para IP vazio");

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Mensagem customizada para IP vazio"));
            Assert.That(result.Code, Is.EqualTo(IpAddressMsg.Code.EX01.ToString()));
        }

        [TestCase("256.256.256.256")]
        [TestCase("999.999.999.999")]
        [TestCase("invalid_ip_format")]
        public void Validate_ShouldReturn_EX02_WhenIpIsInvalid(string input)
        {
            var validator = new IpAddressValidatorStrategy();

            var result = validator.Validate(input, "Formato de IP inválido!");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Formato de IP inválido!"));
            Assert.That(result.Code, Is.EqualTo(IpAddressMsg.Code.EX02.ToString()));
        }

        [TestCase("192.168.0.1")]
        [TestCase("255.255.255.255")]
        [TestCase("8.8.8.8")]
        public void Validate_ShouldReturnSuccess_WhenIpIsValid(string input)
        {
            var validator = new IpAddressValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message); // Supondo que o sucesso não precise de mensagem
        }

        [Test]
        public void Validate_ShouldReturnCustomMessage_WhenCustomMessageIsPassed()
        {
            var validator = new IpAddressValidatorStrategy();

            // Mensagem customizada para erro de IP inválido
            var result = validator.Validate("256.256.256.256", "IP fora do intervalo permitido");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("IP fora do intervalo permitido"));
            Assert.That(result.Code, Is.EqualTo(IpAddressMsg.Code.EX02.ToString()));
        }
    }
}
