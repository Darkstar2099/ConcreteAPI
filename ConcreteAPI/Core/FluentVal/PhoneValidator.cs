using System;
using ConcreteAPI.Core.Entities;
using ConcreteAPI.Core.Constants;
using FluentValidation;

namespace ConcreteAPI.Core.FluentVal
{
    // Implementação da Classe Abstrata do FluentValidation como PhoneValidator
    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Ddd)
                .NotEmpty()
                .Length(PhoneProperties.DddMinLen, PhoneProperties.DddMaxLen);

            RuleFor(x => x.Number)
                .NotEmpty()
                .Length(PhoneProperties.PhoneMinLen, PhoneProperties.PhoneMaxLen);
        }
    }
}