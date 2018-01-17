using ConcreteAPI.Core.Common;
using ConcreteAPI.Core.Constants;
using FluentValidation;

namespace ConcreteAPI.Core.FluentVal
{
    // Implementação da Classe Abstrata do FluentValidation como EmailValidator
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Value)
                .Length(UserProperties.EmailMinLen, UserProperties.EmailMaxLen)
                .EmailAddress();
        }
    }
}