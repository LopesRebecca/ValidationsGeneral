using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Validator.Contact;

namespace ValidationsGeral.Tests.Validator.Date
{
    [TestFixture]
    public class AgeValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsInvalid(string? input)
        {
            var validator = new AgeValidatorStrategy();

            var result = validator.Validate(input, "Idade inválida");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Idade inválida"));
            Assert.That(result.Code, Is.EqualTo(AgeMsg.Code.EX01.ToString()));
        }

        [TestCase("-1")]
        [TestCase("150")]
        public void Validate_ShouldReturn_EX02_WhenAgeOutOfRange(string input)
        {
            var validator = new AgeValidatorStrategy();

            var result = validator.Validate(input, "Idade fora da faixa");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Idade fora da faixa"));
            Assert.That(result.Code, Is.EqualTo(AgeMsg.Code.EX02.ToString()));
        }

        [TestCase("18")]
        [TestCase("20")]
        [TestCase("47")]
        [TestCase("65")]
        public void Validate_ShouldReturnSuccess_WhenAgeIsWithinRange(string input)
        {
            var validator = new AgeValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message);
        }

        [Test]
        public void Validate_ShouldRespectCustomMinMaxAge()
        {
            var validator = new AgeValidatorStrategy(minAge: 18, maxAge: 60);

            var result = validator.Validate("17");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(AgeMsg.Code.EX02.ToString()));
        }
    }
}
