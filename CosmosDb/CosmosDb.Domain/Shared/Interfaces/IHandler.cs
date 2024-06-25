namespace CosmodeDb.Domain.Shared.Interfaces;

public interface IHandler<T> where T : class
{
  public Response Handle(T payload);
}