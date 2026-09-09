namespace Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

public enum OperationError
{
    None = 0,
    InvalidAccountNumber,
    InvalidPinCode,
    InvalidSessionKey,
    InvalidOperationRecord,
    AmountMustBePositive,
    Unauthorized,
    InsufficientFunds,
}