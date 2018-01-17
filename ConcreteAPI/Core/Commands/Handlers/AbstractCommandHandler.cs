using ConcreteAPI.Core.Common;
using FluentValidation.Results;

namespace ConcreteAPI.Core.Commands.Handlers
{
    public abstract class AbstractCommandHandler
    {
        protected CommandReturn Success(object data = null)
        {
            if (data == null)
                return new CommandReturn();
            return new CommandReturn(data);
        }

        protected CommandReturn Error(string errorMessage)
        {
            return new CommandReturn(errorMessage);
        }

        protected CommandReturn Error(ValidationResult validationResult)
        {
            return new CommandReturn(validationResult.Errors);
        }
    }
}