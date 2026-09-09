namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.Accounts;

public class DepositRequest
{
    public Guid UserSessionKey { get; set; }

    public decimal Amount { get; set; }
}