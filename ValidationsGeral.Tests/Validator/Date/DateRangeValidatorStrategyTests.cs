using ValidationsGeneral.Enum.Date;
using ValidationsGeneral.Validator.Date;

namespace ValidationsGeral.Tests.Validator.Date
{
    [TestFixture]
    public class DateRangeValidatorStrategyTests
    {
        [Test]
        public void Validate_ShouldReturn_EX01_WhenInputIsEmpty()
        {
            var validator = new DateRangeValidatorStrategy();

            var result = validator.Validate("", "Data não pode ser vazia");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(DateRangeMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo("Data não pode ser vazia"));
        }

        [Test]
        public void Validate_ShouldReturn_EX02_WhenInputIsInvalid()
        {
            var validator = new DateRangeValidatorStrategy();

            var result = validator.Validate("data inválida", "Formato inválido");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(DateRangeMsg.Code.EX02.ToString()));
            Assert.That(result.Message, Is.EqualTo("Formato inválido"));
        }

        [Test]
        public void Validate_ShouldReturn_EX03_WhenDateIsBeforeMin()
        {
            var min = new DateTime(2020, 1, 1);
            var validator = new DateRangeValidatorStrategy(minDate: min);

            var result = validator.Validate("01/01/2019", "Antes do mínimo");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(DateRangeMsg.Code.EX03.ToString()));
            Assert.That(result.Message, Is.EqualTo("Antes do mínimo"));
        }

        [Test]
        public void Validate_ShouldReturn_EX04_WhenDateIsAfterMax()
        {
            var max = new DateTime(2030, 12, 31);
            var validator = new DateRangeValidatorStrategy(maxDate: max);

            var result = validator.Validate("01/01/2035", "Depois do máximo");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(DateRangeMsg.Code.EX04.ToString()));
            Assert.That(result.Message, Is.EqualTo("Depois do máximo"));
        }

        [Test]
        public void Validate_ShouldReturnSuccess_WhenDateIsWithinRange()
        {
            var validator = new DateRangeValidatorStrategy(
                minDate: new DateTime(2020, 1, 1),
                maxDate: new DateTime(2030, 12, 31)
            );

            var result = validator.Validate("01/01/2025");

            Assert.IsTrue(result.IsValid);
        }
    }
}