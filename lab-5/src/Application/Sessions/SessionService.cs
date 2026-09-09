using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.Security;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Sessions;

public class SessionService
{
    private ISessionStorage SessStorage { get; }

    private ISessionFactory Factory { get; }

    private IAccountStorage AccStorage { get; }

    private ISystemPassword SystemPassword { get; }

    public SessionService(
        ISessionStorage sessStorage,
        ISessionFactory factory,
        IAccountStorage accStorage,
        ISystemPassword systemPassword)
    {
        SessStorage = sessStorage ?? throw new ArgumentNullException(nameof(sessStorage));
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        AccStorage = accStorage ?? throw new ArgumentNullException(nameof(accStorage));
        SystemPassword = systemPassword ?? throw new ArgumentNullException(nameof(systemPassword));
    }

    public OperationResultWithValue<SessionKey> CreateUserSession(string accountNumber, string pinCode)
    {
        OperationResultWithValue<AccountNumber> accountResult = AccountNumber.Create(accountNumber);
        if (!accountResult.IsSuccess)
            return OperationResultWithValue<SessionKey>.Fail(accountResult.Error);

        OperationResultWithValue<PinCode> pinResult = PinCode.Create(pinCode);
        if (!pinResult.IsSuccess)
            return OperationResultWithValue<SessionKey>.Fail(pinResult.Error);

        Account? account = AccStorage.FindByNumber(accountResult.GetValue);
        if (account is null)
            return OperationResultWithValue<SessionKey>.Fail(OperationError.InvalidAccountNumber);

        if (!account.IsPinValid(pinResult.GetValue))
            return OperationResultWithValue<SessionKey>.Fail(OperationError.InvalidPinCode);

        Session session = Factory.CreateUserSession(account.Number);

        SessStorage.Add(session);

        return OperationResultWithValue<SessionKey>.Success(session.Key);
    }

    public OperationResultWithValue<SessionKey> CreateAdminSession(string password)
    {
        string realPassword = SystemPassword.GetPassword();

        if (!string.Equals(password, realPassword, StringComparison.Ordinal))
            return OperationResultWithValue<SessionKey>.Fail(OperationError.Unauthorized);

        Session session = Factory.CreateAdminSession();

        SessStorage.Add(session);

        return OperationResultWithValue<SessionKey>.Success(session.Key);
    }
}