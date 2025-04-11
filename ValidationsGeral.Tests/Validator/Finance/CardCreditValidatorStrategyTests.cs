using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Exception.Finance;
using ValidationsGeneral.Validator.Finance;

namespace ValidationsGeral.Tests.Validator.Finance
{
    [TestFixture]
    public class CardCreditValidatorStrategyTests
    {
        private readonly CardCreditValidatorStrategy _strategy;

        public CardCreditValidatorStrategyTests()
        {
            _strategy = new CardCreditValidatorStrategy(new CardCreditException());
        }

        #region InvalidCard
        [TestCase("4111111", "Cartão inválido, verifique")]
        [TestCase("4111111", "EX01")]
        public void Validate_InvalidCard_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CardCreditMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }

        [TestCase("", "EX02")]
        [TestCase(" ", "EX02")]
        [TestCase("", "EX02 - Cartão em branco, verifique")]
        public void Validate_EmptyCard_ReturnsInvalid(string input, string customMessage)
        {
            // Act
            var result = _strategy!.Validate(input, customMessage);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(CardCreditMsg.Code.EX02.ToString()));
            Assert.That(result.Message, Is.EqualTo(customMessage));
        }
        #endregion

        #region ValidCard
        [TestCase("5494 1442 4972 7767")]
        [TestCase("6062 8237 6993 7167")]
        public void Validate_ValidCard_ReturnsInvalid(string input)
        {
            // Act
            var result = _strategy!.Validate(input);

            // Assert
            Assert.IsTrue(result.IsValid);
        }
        #endregion
    }
}
