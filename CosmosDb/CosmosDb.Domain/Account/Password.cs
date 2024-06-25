using System.Security.Cryptography;
using CosmodeDb.Domain.Shared;

namespace CosmosDb.Domain.Account;

public sealed class Password(string password) : ValueObject
{
  #region private properties

  private const int ITERATION_COUNT = 1000;
  private const short SALT_SIZE_IN_BYTES = 16;
  private const char SPLIT_CHAR = '.';
  private const int KEY_SIZE = 32;

  private string _hash = string.Empty;
  private readonly string _saltKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(SALT_SIZE_IN_BYTES));

  #endregion

  public string Salt => _saltKey;

  public override bool IsValid()
  {
    var split = _hash.Split(SPLIT_CHAR);

    var saltFromDb = Convert.FromBase64String(split[0]);
    var hashFromDb = Convert.FromBase64String(split[1]);
    
    var algorithm = new Rfc2898DeriveBytes(
      password,
      saltFromDb,
      iterations: ITERATION_COUNT,
      HashAlgorithmName.SHA256
    );

    var keyToCheck = algorithm.GetBytes(KEY_SIZE);

    return keyToCheck.SequenceEqual(hashFromDb);
  }

  public string GenerateHash(string saltKey)
  {
    var algorithm = new Rfc2898DeriveBytes(
      password: password,
      salt: Convert.FromBase64String(saltKey),
      iterations: ITERATION_COUNT,
      HashAlgorithmName.SHA256
    );

    var key = Convert.ToBase64String(algorithm.GetBytes(KEY_SIZE));
    var salt = Convert.ToBase64String(algorithm.Salt);

    return $"{salt}{SPLIT_CHAR}{key}";
  }

  public void SetHashedPasswordToCompare(string hashedPassword) => _hash = hashedPassword;
}