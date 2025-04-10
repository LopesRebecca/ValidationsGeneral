using ValidationsGeneral.Enum.Identity;
using ValidationsGeneral.Exception.Finance;
using ValidationsGeneral.Validator.Finance;

namespace ValidationsGeneral.Tests.Validator.Identity
{
    public class CardCreditValidatorStrategyTests
    {
        private readonly CardCreditValidatorStrategy _defaultValidator = new();
        private readonly CardCreditValidatorStrategy _withResolver;

        public CardCreditValidatorStrategyTests()
        {
            _withResolver = new CardCreditValidatorStrategy(new CardCreditException());
        }

        [Theory]
        [InlineData("4111 1111 1111", true)]
        [InlineData("0000 0000 0000", false)]
        [InlineData("99999999999", false)]
        [InlineData("", false)]
        public void Validate_WithDefaultValidator_ShouldReturnCorrectResult(string cardCredit, bool expectedValid)
        {
            var result = _defaultValidator.Validate(cardCredit);
            Assert.Equal(expectedValid, result.IsValid);
        }
       
    }
}
