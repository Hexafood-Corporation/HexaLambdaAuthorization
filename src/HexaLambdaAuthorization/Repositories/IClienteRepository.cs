using HexaLambdaAuthorization.Models;

namespace HexaLambdaAuthorization.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByCPFAsync(string cpf);
    }
}
