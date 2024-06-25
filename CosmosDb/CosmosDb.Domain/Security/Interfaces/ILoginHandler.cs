using CosmodeDb.Domain.Shared.Interfaces;
using CosmosDb.Domain.Security.Requests;

namespace CosmodeDb.Domain.Security.Interfaces;

public interface ILoginHandler : IHandler<LoginRequest>
{

}