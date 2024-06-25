namespace CosmodeDb.Domain.Shared;

public class Response
{
  public bool Success { get; set; } = false;
  
  public string Message { get; set; }

  public static Response Failed(string message)
  {
    return new() 
    {
      Success = false,
      Message = message
    };
  }

  public static Response Succeed(string message)
  {
    return new()
    {
      Success = true,
      Message = message
    };
  }
}

public class Response<T> : Response
{
  public T Content { get; set; }

  public Response<T> Succeed(T content)
  {
    return new() 
    {
      Success = true,
      Message = string.Empty,
      Content = content
    };
  }
}