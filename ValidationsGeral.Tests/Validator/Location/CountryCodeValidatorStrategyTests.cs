using ValidationsGeneral.Enum.Location;
using ValidationsGeneral.Validator.Location;

namespace ValidationsGeral.Tests.Validator.Location
{
    public class CountryCodeValidatorStrategyTests
    {
        private CountryCodeValidatorStrategy _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CountryCodeValidatorStrategy();
        }

        [TestCase("BR", true)]
        [TestCase("us", true)]
        [TestCase("DE", true)]
        [TestCase("XX", false)]
        [TestCase("123", false)]
        [TestCase("", false)]
        public void Validate_ShouldReturnExpectedResult(string input, bool expectedIsValid)
        {
            var result = _validator.Validate(input ?? "");

            Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
        }

        [Test]
        public void Validate_ShouldReturnErrorCode_WhenInputIsEmpty()
        {
            var result = _validator.Validate("");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CountryCodeMsg.Code.EX01.ToString())); 
        }

        [Test]
        public void Validate_ShouldReturnErrorCode_WhenCountryCodeIsInvalid()
        {
            var result = _validator.Validate("ZZ");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CountryCodeMsg.Code.EX02.ToString()));
        }

        [Test]
        public void Validate_ShouldUseCustomMessage_WhenProvided()
        {
            var customMessage = "Código de país inválido.";
            var result = _validator.Validate("XYZ", customMessage);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }
    }
}
