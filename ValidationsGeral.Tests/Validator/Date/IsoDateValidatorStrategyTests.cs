using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Validator.Contact;

namespace ValidationsGeral.Tests.Validator.Date
{
    [TestFixture]
    public class IsoDateValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsNullOrEmpty(string? input)
        {
            var validator = new IsoDateValidatorStrategy();

            var result = validator.Validate(input, "Data não pode ser vazia");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(IsoDateMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo("Data não pode ser vazia"));
        }

        [TestCase("01/01/2023")]
        [TestCase("2023-13-01")]
        [TestCase("2023-01-32")]
        [TestCase("2023-1-1")]
        public void Validate_ShouldReturn_EX02_WhenDateIsInvalid(string input)
        {
            var validator = new IsoDateValidatorStrategy();

            var result = validator.Validate(input, "Formato de data inválido");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(IsoDateMsg.Code.EX02.ToString()));
            Assert.That(result.Message, Is.EqualTo("Formato de data inválido"));
        }

        [TestCase("2023-01-01")]
        [TestCase("1999-12-31")]
        [TestCase("2025-04-18")]
        public void Validate_ShouldReturnSuccess_WhenDateIsValid(string input)
        {
            var validator = new IsoDateValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message);
        }
    }
}