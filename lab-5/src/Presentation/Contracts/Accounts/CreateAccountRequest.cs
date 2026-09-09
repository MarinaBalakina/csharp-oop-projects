namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.Accounts;

public class CreateAccountRequest
{
    public Guid AdminSessionKey { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string PinCode { get; set; } = string.Empty;

    public decimal InitialBalance { get; set; }
}