namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.Sessions;

public class UserSessionRequest
{
    public string AccountNumber { get; set; } = string.Empty;

    public string PinCode { get; set; } = string.Empty;
}