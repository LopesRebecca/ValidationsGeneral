using ValidationsGeneral.Enum.Location;
using ValidationsGeneral.Validator.Location;

namespace ValidationsGeral.Tests.Validator.Location
{
    [TestFixture]
    public class TimezoneValidatorStrategyTests
    {
        private TimezoneValidatorStrategy _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new TimezoneValidatorStrategy();
        }

        [TestCase("UTC", true)]
        [TestCase("America/Sao_Paulo", true)]
        [TestCase("Europe/Berlin", true)]
        [TestCase("Asia/Tokyo", true)]
        [TestCase("Invalid/Timezone", false)]
        [TestCase("utc", true)] // case-insensitive
        [TestCase("", false)]
        [TestCase("   ", false)]
        [TestCase(null, false)]
        public void Validate_ShouldReturnExpectedResult(string? input, bool expectedIsValid)
        {
            var result = _validator.Validate(input ?? "");

            Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
        }

        [Test]
        public void Validate_ShouldReturnErrorCode_WhenInputIsEmpty()
        {
            var result = _validator.Validate("");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo("EX01"));
        }

        [Test]
        public void Validate_ShouldReturnErrorCode_WhenTimezoneIsInvalid()
        {
            var result = _validator.Validate("Mars/Olympus");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(TimezoneMsg.Code.EX02.ToString())); 
        }

        [Test]
        public void Validate_ShouldUseCustomMessage_WhenProvided()
        {
            var customMessage = "Fuso horário inválido";
            var result = _validator.Validate("PlanetaX/AgoraVai", customMessage);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }
    }
}