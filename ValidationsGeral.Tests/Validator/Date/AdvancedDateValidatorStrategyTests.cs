using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Validator.Date;

namespace ValidationsGeral.Tests.Validator.Date
{
    [TestFixture]
    public class AdvancedDateValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsEmpty(string? input)
        {
            var validator = new AdvancedDateValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(AdvancedDateMsg.Code.EX01.ToString()));
        }

        [Test]
        public void Validate_ShouldReturn_EX02_WhenFormatIsInvalid()
        {
            var validator = new AdvancedDateValidatorStrategy(
                culture: "en-US",
                formats: new[] { "MM/dd/yyyy" }
            );

            var result = validator.Validate("2025-12-31");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(AdvancedDateMsg.Code.EX02.ToString()));
        }

        [Test]
        public void Validate_ShouldReturn_EX03_WhenDateIsBeforeMin()
        {
            var validator = new AdvancedDateValidatorStrategy(
                minDate: new DateTime(2020, 01, 01),
                formats: new[] { "dd/MM/yyyy" },
                culture: "pt-BR"
            );

            var result = validator.Validate("31/12/2010");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(AdvancedDateMsg.Code.EX03.ToString()));
        }

        [Test]
        public void Validate_ShouldReturn_EX04_WhenDateIsAfterMax()
        {
            var validator = new AdvancedDateValidatorStrategy(
                maxDate: new DateTime(2030, 12, 31),
                formats: new[] { "dd/MM/yyyy" },
                culture: "pt-BR"
            );

            var result = validator.Validate("01/01/2040");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(AdvancedDateMsg.Code.EX04.ToString()));
        }

        [Test]
        public void Validate_ShouldReturnSuccess_WhenDateIsWithinRange()
        {
            var validator = new AdvancedDateValidatorStrategy(
                formats: new[] { "dd/MM/yyyy", "yyyy-MM-dd" },
                culture: "pt-BR",
                minDate: new DateTime(2020, 01, 01),
                maxDate: new DateTime(2030, 12, 31)
            );

            var result = validator.Validate("15/08/2025");

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message);
        }

        [Test]
        public void Validate_ShouldAcceptMultipleFormats()
        {
            var validator = new AdvancedDateValidatorStrategy(
                formats: new[] { "yyyy-MM-dd", "dd-MM-yyyy" },
                culture: "pt-BR"
            );

            var result1 = validator.Validate("2025-12-01");
            var result2 = validator.Validate("01-12-2025");

            Assert.IsTrue(result1.IsValid);
            Assert.IsTrue(result2.IsValid);
        }
    }
}
