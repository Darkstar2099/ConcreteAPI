using System.Threading.Tasks;
using ConcreteAPI.Core.Common;

namespace ConcreteAPI.Core.Commands.Interfaces
{
    // Interface para implementar o Design Pattern Domain Command (Handlers)
    public interface ICommandHandler <in TCommand> where TCommand : ICommand
    {
        // Assinatura do Método de Execução Assíncrona
        Task<CommandReturn> ExecuteAsync(TCommand command);
    }
}
