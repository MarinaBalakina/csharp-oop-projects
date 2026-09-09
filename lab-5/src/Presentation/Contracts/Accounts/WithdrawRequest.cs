namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.Accounts;

public class WithdrawRequest
{
    public Guid UserSessionKey { get; set; }

    public decimal Amount { get; set; }
}