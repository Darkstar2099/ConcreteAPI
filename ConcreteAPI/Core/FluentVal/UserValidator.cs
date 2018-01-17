using System;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Constants;
using FluentValidation;

namespace ConcreteAPI.Core.FluentVal
{
    // Implementação da Classe Abstrata do FluentValidation como UserValidator
    public class UserValidator :AbstractValidator<User>
    {

        public UserValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(UserProperties.NameMinLen, UserProperties.NameMaxLen);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(UserProperties.EmailMinLen, UserProperties.EmailMaxLen);

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.CreationDate);

            RuleFor(x => x.UpdateDate);

            RuleFor(x => x.LastLoginDate);

            RuleFor(x => x.Token);

            RuleForEach(x => x.Phones)
                .NotNull();

            RuleFor(x => x.Phones)
                .SetCollectionValidator(new PhoneValidator());
        }
    }
}