using ValidationsGeneral.Interface;
using ValidationsGeneral.Validator.Finance;
using ValidationsGeneral.Validator.Identity;

namespace ValidationsGeneral.Factory
{
    public enum DocumentType
    {
        Cpf,
        Cnpj,
        CardCredit
    }

    public static class ValidatorFactory
    {
        public static IValidatorStrategy Create(DocumentType tipo)
        {
            return tipo switch
            {
                DocumentType.Cpf => new CpfValidatorStrategy(),
                DocumentType.Cnpj => new CnpjValidatorStrategy(),
                DocumentType.CardCredit => new CardCreditValidatorStrategy(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
