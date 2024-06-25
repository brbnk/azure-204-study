using System.Text.RegularExpressions;
using CosmodeDb.Domain.Shared;

namespace CosmodeDb.Domain.Account;

public sealed partial class Email(string address) : ValueObject
{
  public string Address { get; private set; }

  public override bool IsValid()
  {
    var isValidFormat = EmailFormatRegex().IsMatch(address);

    if (isValidFormat)
    {
      Address = address.Trim().ToLower();
    }

    return isValidFormat;
  }

  [GeneratedRegex(@"^[\w.-]+@(?=[a-z\d][^.]*\.)[a-z\d.-]*[^.]$", RegexOptions.IgnoreCase)]
  private static partial Regex EmailFormatRegex();
}   