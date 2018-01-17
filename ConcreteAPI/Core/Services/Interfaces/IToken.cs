using ConcreteAPI.Core.Entities;

namespace ConcreteAPI.Core.Services.Interfaces
{
    public interface IToken
    {
        string GenerateToken(User user);
    }
}
