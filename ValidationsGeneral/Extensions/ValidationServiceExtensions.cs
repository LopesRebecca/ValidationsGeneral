using Microsoft.Extensions.DependencyInjection;
using ValidationsGeneral.Validator.Contact;
using ValidationsGeneral.Validator.Date;
using ValidationsGeneral.Validator.Finance;
using ValidationsGeneral.Validator.Identity;
using ValidationsGeneral.Validator.Location;
using ValidationsGeneral.Validators.Strategies;

namespace ValidationsGeneral.Extensions
{
    public static class ValidationServiceExtensions
    {
        /// <summary>
        /// Registra todas as estratégias 
        /// de validação disponíveis no container de injeção de dependência.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <returns>Instância modificada de <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddValidationStrategies(this IServiceCollection services)
        {
            #region Contact
            services.AddTransient<EmailValidatorStrategy>();
            services.AddTransient<IpAddressValidatorStrategy>();
            services.AddTransient<PhoneValidatorStrategy>();
            services.AddTransient<UrlValidatorStrategy>();
            #endregion

            #region Date
            services.AddTransient<AgeValidatorStrategy>();
            services.AddTransient<IsoDateValidatorStrategy>();
            services.AddTransient<AdvancedDateValidatorStrategy>();
            services.AddTransient<FlexibleDateValidatorStrategy>();
            services.AddTransient<DateRangeValidatorStrategy>();
            #endregion

            #region Finance
            services.AddTransient<CardCreditValidatorStrategy>();
            #endregion

            #region Identity
            services.AddTransient<CpfValidatorStrategy>();
            services.AddTransient<CnpjValidatorStrategy>();
            #endregion

            #region Location
            services.AddTransient<CountryCodeValidatorStrategy>();
            services.AddTransient<PostalCodeValidatorStrategy>();
            services.AddTransient<TimezoneValidatorStrategy>();
            #endregion

            return services;
        }
    }
}
