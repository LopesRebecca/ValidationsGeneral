using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Validator.Date;

namespace ValidationsGeral.Tests.Validator.Date
{
    [TestFixture]
    public class FlexibleDateValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsNullOrEmpty(string? input)
        {
            var validator = new FlexibleDateValidatorStrategy(culture: "en-US");

            var result = validator.Validate(input, "Data vazia");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(FlexibleDateMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo("Data vazia"));
        }

        [TestCase("31/12/2023", "en-US")]
        [TestCase("12.31.2023", "pt-BR")]
        public void Validate_ShouldReturn_EX02_WhenInputIsInvalidForCulture(string input, string culture)
        {
            var validator = new FlexibleDateValidatorStrategy(culture: culture);

            var result = validator.Validate(input, "Formato inválido");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(FlexibleDateMsg.Code.EX02.ToString()));
            Assert.That(result.Message, Is.EqualTo("Formato inválido"));
        }

        [TestCase("12/31/2023", "en-US")]
        [TestCase("31/12/2023", "pt-BR")]
        [TestCase("31.12.2023", "de-DE")]
        [TestCase("2023/12/31", "ja-JP")]
        public void Validate_ShouldReturnSuccess_WhenDateIsValidForCulture(string input, string culture)
        {
            var validator = new FlexibleDateValidatorStrategy(culture: culture);

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
        }

        [Test]
        public void Validate_ShouldAcceptCustomFormats()
        {
            var formats = new[] { "yyyy/MM/dd", "dd-MM-yyyy" };
            var validator = new FlexibleDateValidatorStrategy(culture: "pt-BR", customFormats: formats);

            var result = validator.Validate("2023/12/01");

            Assert.IsTrue(result.IsValid);
        }
    }
}