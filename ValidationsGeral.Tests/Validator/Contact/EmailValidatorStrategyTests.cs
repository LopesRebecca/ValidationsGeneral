using ValidationsGeneral.Enum.Contact;
using ValidationsGeneral.Validator.Contact;

namespace ValidationsGeral.Tests.Validator.Contact
{
    [TestFixture]
    public class EmailValidatorStrategyTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ShouldReturn_EX01_WhenInputIsNullOrEmpty(string? input)
        {
            var validator = new EmailValidatorStrategy();

            var result = validator.Validate(input, "Mensagem customizada para entrada vazia");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Code, Is.EqualTo(EmailMsg.Code.EX01.ToString()));
            Assert.That(result.Message, Is.EqualTo("Mensagem customizada para entrada vazia"));
        }

        [TestCase("emailsemarroba.com")]
        [TestCase("email@sem-dominio")]
        [TestCase("@semusuario.com")]
        [TestCase("nome@.com")]
        [TestCase("nome@dominio.")]
        public void Validate_ShouldReturn_EX02_WhenEmailIsInvalid(string input)
        {
            var validator = new EmailValidatorStrategy();

            var result = validator.Validate(input, "Formato de email inválido!");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Formato de email inválido!"));
            Assert.That(result.Code, Is.EqualTo(EmailMsg.Code.EX02.ToString()));
        }

        [TestCase("teste@email.com")]
        [TestCase("usuario.nome@dominio.com")]
        [TestCase("user_name123@sub.dominio.co.uk")]
        public void Validate_ShouldReturnSuccess_WhenEmailIsValid(string input)
        {
            var validator = new EmailValidatorStrategy();

            var result = validator.Validate(input);

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message); // Supondo que o sucesso não precise de mensagem
        }

        [Test]
        public void Validate_ShouldReturn_EX03_WhenCustomRuleFails()
        {
            // Regra personalizada que só permite domínios ".gov.br"
            Func<string, bool> customRule = email => email.EndsWith(".gov.br");
            var validator = new EmailValidatorStrategy(customRule: customRule);

            var result = validator.Validate("email@dominio.com", "Email não está no domínio permitido!");

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Message, Is.EqualTo("Email não está no domínio permitido!"));
            Assert.That(result.Code, Is.EqualTo(EmailMsg.Code.EX03.ToString()));
        }

        [Test]
        public void Validate_ShouldReturnSuccess_WhenCustomRulePasses()
        {
            // Regra personalizada que só permite domínios ".gov.br"
            Func<string, bool> customRule = email => email.EndsWith(".gov.br");
            var validator = new EmailValidatorStrategy(customRule: customRule);

            var result = validator.Validate("email@dominio.gov.br");

            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.Code);
            Assert.IsNull(result.Message);
        }
    }
}
