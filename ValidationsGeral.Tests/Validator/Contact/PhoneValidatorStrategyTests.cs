using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Validator.Contact;

namespace ValidationsGeneral.Tests.Validators
{
    [TestFixture]
    public class PhoneValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsNullOrEmpty(string? input)
        {
            // Arrange
            var validator = new PhoneValidatorStrategy();

            // Act
            var result = validator.Validate(input, "Mensagem customizada para telefone vazio");

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Mensagem customizada para telefone vazio"));
            Assert.That(result.Code, Is.EqualTo(PhoneMsg.Code.EX01.ToString()));
        }

        [TestCase("12345")]
        [TestCase("1234567890123456")]
        [TestCase("abcdefghij")]
        [TestCase("123-456-7890")]
        [TestCase("+12-345-678-901")]
        public void Validate_ShouldReturn_EX02_WhenPhoneIsInvalid(string input)
        {
            var validator = new PhoneValidatorStrategy();

            var result = validator.Validate(input, "Formato de telefone inválido!");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Formato de telefone inválido!"));
            Assert.That(result.Code, Is.EqualTo(PhoneMsg.Code.EX02.ToString()));
        }

        [TestCase("+1234567890")]
        [TestCase("9876543210")]
        [TestCase("+441632960961")]
        [TestCase("03123456789")]
        [TestCase("123456789012345")]
        public void Validate_ShouldReturnSuccess_WhenPhoneIsValid(string input)
        {
            var validator = new PhoneValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message); // Supondo que o sucesso não precise de mensagem
        }

        [Test]
        public void Validate_ShouldReturnCustomMessage_WhenCustomMessageIsPassed()
        {
            var validator = new PhoneValidatorStrategy();

            // Mensagem customizada para erro de telefone inválido
            var result = validator.Validate("12345", "Número de telefone fora do formato esperado");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Número de telefone fora do formato esperado"));
            Assert.That(result.Code, Is.EqualTo(PhoneMsg.Code.EX02.ToString()));
        }
    }
}
