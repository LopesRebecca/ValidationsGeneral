using ValidationsGeneral.Enum.Location;
using ValidationsGeneral.Validator.Location;

namespace ValidationsGeral.Tests.Validator.Location
{
    [TestFixture]
    public class PostalCodeValidatorStrategyTests
    {
        private PostalCodeValidatorStrategy _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new PostalCodeValidatorStrategy();
        }

        [TestCase("12345-678", "BR", true)]
        [TestCase("12345678", "BR", false)]
        [TestCase("12345", "US", true)]
        [TestCase("12345-6789", "US", true)]
        [TestCase("K1A 0B1", "CA", true)]
        [TestCase("SW1A 1AA", "GB", true)]
        [TestCase("1234-567", "PT", true)]
        [TestCase("00-001", "PL", true)]
        [TestCase("ABCDE", "BR", false)]
        public void Validate_ShouldReturnExpectedResult(string input, string countryCode, bool expectedIsValid)
        {
            var validator = new PostalCodeValidatorStrategy(null, countryCode);
            var result = validator.Validate(input);

            Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
        }

        [Test]
        public void Validate_ShouldReturnError_WhenInputIsEmpty()
        {
            var result = _validator.Validate(string.Empty);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(PostalCodeMsg.Code.EX01.ToString()));
        }

        [Test]
        public void Validate_ShouldReturnError_WhenCountryCodeIsUnsupported()
        {
            var validator = new PostalCodeValidatorStrategy(null, "ZZ");
            var result = validator.Validate("12345");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(PostalCodeMsg.Code.EX02.ToString()));
        }

        [Test]
        public void Validate_ShouldUseCustomMessage_WhenProvided()
        {
            var invalidInput = "invalid";
            var customMessage = "Formato inválido!";
            var result = _validator.Validate(invalidInput, customMessage);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }
    }
}