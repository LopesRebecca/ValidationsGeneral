using NUnit.Framework;
using ValidationsGeneral.Exception.identity;
using ValidationsGeneral.Tests.CaseTestEvente.Identity;

namespace ValidationsGeneral.Validator.Identity.Tests
{
    [TestFixture]
    public class CpfValidatorStrategyTests
    {
        private readonly CpfValidatorStrategy _defaultValidator = new();
        private readonly CpfValidatorStrategy _withResolver;

        public CpfValidatorStrategyTests()
        {
            _withResolver = new CpfValidatorStrategy(new CpfException());
        }

        [Test, TestCaseSource(typeof(CpfCaseTest), "InvalidCPF")]
        public void InvalidTestsCPF(string cpf, string erroMenssage)
        {
            Assert.Throws(
            typeof(ArgumentException),
                () => { _withResolver.Validate(cpf, erroMenssage); }
            );
        }

        [Test, TestCaseSource(typeof(CpfCaseTest), "ValidCPF")]
        public void ValidTestsCPF(string cpf)
        {
            Assert.Throws(
            typeof(ArgumentException),
                () => { _defaultValidator.Validate(cpf); }
            );
        }
    }
}