using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Validators.Strategies;
    
namespace ValidationsGeral.Tests.Validator.Contact    
{
    [TestFixture]
    public class UrlValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsNullOrEmpty(string? input)
        {
            // Arrange
            var validator = new UrlValidatorStrategy();

            // Act
            var result = validator.Validate(input);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(UrlMsg.Code.EX01.ToString()));
        }

        [TestCase("notaurl")]
        [TestCase("http//semdois.pontos.com")]
        [TestCase("://semprotocolo")]
        public void Validate_ShouldReturn_EX02_WhenUrlIsMalformed(string input)
        {
            var validator = new UrlValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(UrlMsg.Code.EX02.ToString()));
        }

        [Test]
        public void Validate_ShouldReturn_EX03_WhenCustomRuleFails()
        {
            Func<Uri, bool> customRule = uri => uri.Host.EndsWith(".gov.br");
            var validator = new UrlValidatorStrategy(customRule: customRule);

            var result = validator.Validate("https://sitecomum.com");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(UrlMsg.Code.EX03.ToString()));
        }

        [Test]
        public void Validate_ShouldReturn_EX04_WhenSchemeIsNotAllowed()
        {
            var validator = new UrlValidatorStrategy(allowedSchemes: new[] { "https" });

            var result = validator.Validate("ftp://meusite.com");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(UrlMsg.Code.EX04.ToString()));
        }

        [TestCase("http://site.com")]
        [TestCase("https://seguro.com")]
        public void Validate_ShouldReturnSuccess_ForValidUrls_WithDefaultSchemes(string input)
        {
            var validator = new UrlValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
        }

        [Test]
        public void Validate_ShouldReturnSuccess_WhenCustomRulePasses()
        {
            Func<Uri, bool> customRule = uri => uri.Host.EndsWith(".gov.br");
            var validator = new UrlValidatorStrategy(customRule: customRule);

            var result = validator.Validate("https://portal.gov.br");

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
        }

        [Test]
        public void Validate_ShouldReturnSuccess_WhenSchemeIsAllowed()
        {
            var validator = new UrlValidatorStrategy(allowedSchemes: new[] { "ftp" });

            var result = validator.Validate("ftp://meusite.com");

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
        }
    }
}