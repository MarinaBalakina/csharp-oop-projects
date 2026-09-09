using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.Time;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Accounts;

public class AccountService
{
    private IAccountStorage AccServise { get; }

    private ISessionStorage SessStorage { get; }

    private IHistoryStorage HistStorage { get; }

    private IAccountFactory Factory { get; }

    private IClock Timer { get; }

    public AccountService(
        IAccountStorage accStorage,
        ISessionStorage sessStorage,
        IHistoryStorage histStorage,
        IAccountFactory factory,
        IClock timer)
    {
        AccServise = accStorage ?? throw new ArgumentNullException(nameof(accStorage));
        SessStorage = sessStorage ?? throw new ArgumentNullException(nameof(sessStorage));
        HistStorage = histStorage ?? throw new ArgumentNullException(nameof(histStorage));
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Timer = timer ?? throw new ArgumentNullException(nameof(timer));
    }

    public OperationResult CreateAccount(
        SessionKey adminSessionKey,
        string accountNumber,
        string pinCode,
        decimal balance)
    {
        ISession? session = SessStorage.FindByKey(adminSessionKey);

        if (session is not AdminSession)
            return OperationResult.Fail(OperationError.Unauthorized);

        OperationResultWithValue<Account> accountResult = Factory.Create(accountNumber, pinCode, balance);
        if (!accountResult.IsSuccess)
            return OperationResult.Fail(accountResult.Error);

        Account account = accountResult.GetValue;
        AccServise.Add(account);

        TryWriteHistory(
            account.Number,
            operationName: "CreateAccount",
            amount: null,
            balanceAfter: account.ViewBalance);

        return OperationResult.Success();
    }

    public OperationResultWithValue<MoneyAmount> GetBalance(SessionKey userSessionKey)
    {
        ISession? session = SessStorage.FindByKey(userSessionKey);
        if (session is not UserSession userSession)
            return OperationResultWithValue<MoneyAmount>.Fail(OperationError.Unauthorized);

        AccountNumber accountNum = userSession.AccNumber;
        Account? account = AccServise.FindByNumber(accountNum);
        if (account is null)
            return OperationResultWithValue<MoneyAmount>.Fail(OperationError.InvalidAccountNumber);

        TryWriteHistory(
            account.Number,
            operationName: "BalanceView",
            amount: null,
            balanceAfter: account.ViewBalance);

        return OperationResultWithValue<MoneyAmount>.Success(account.ViewBalance);
    }

    public OperationResult Deposit(SessionKey userSessionKey, decimal amount)
    {
        ISession? session = SessStorage.FindByKey(userSessionKey);
        if (session is not UserSession userSession)
            return OperationResult.Fail(OperationError.Unauthorized);

        AccountNumber accountNum = userSession.AccNumber;
        Account? account = AccServise.FindByNumber(accountNum);
        if (account is null)
            return OperationResult.Fail(OperationError.InvalidAccountNumber);

        OperationResultWithValue<TransactionAmount> amountResult = TransactionAmount.Create(amount);
        if (!amountResult.IsSuccess)
            return OperationResult.Fail(amountResult.Error);

        OperationResult depositResult = account.IncreaseBalance(amountResult.GetValue);
        if (!depositResult.IsSuccess)
            return depositResult;

        AccServise.Update(account);

        TryWriteHistory(
            account.Number,
            operationName: "Deposit",
            amount: null,
            balanceAfter: account.ViewBalance);

        return OperationResult.Success();
    }

    public OperationResult Withdraw(SessionKey userSessionKey, decimal amount)
    {
        ISession? session = SessStorage.FindByKey(userSessionKey);
        if (session is not UserSession userSession)
            return OperationResult.Fail(OperationError.Unauthorized);

        AccountNumber accountNum = userSession.AccNumber;
        Account? account = AccServise.FindByNumber(accountNum);
        if (account is null)
            return OperationResult.Fail(OperationError.InvalidAccountNumber);

        OperationResultWithValue<TransactionAmount> amountResult = TransactionAmount.Create(amount);
        if (!amountResult.IsSuccess)
            return OperationResult.Fail(amountResult.Error);

        OperationResult withdrawResult = account.DecreaseBalance(amountResult.GetValue);
        if (!withdrawResult.IsSuccess)
            return withdrawResult;

        AccServise.Update(account);

        TryWriteHistory(
            account.Number,
            operationName: "CreateAccount",
            amount: null,
            balanceAfter: account.ViewBalance);

        return OperationResult.Success();
    }

    private void TryWriteHistory(
        AccountNumber accountNumber,
        string operationName,
        TransactionAmount? amount,
        MoneyAmount balanceAfter)
    {
        OperationResultWithValue<OperationName> nameResult = OperationName.Create(operationName);
        if (!nameResult.IsSuccess) return;

        DateTimeOffset occurredAt = Timer.Now();
        OperationResultWithValue<OperationRecord> recordResult =
            OperationRecord.Create(accountNumber, nameResult.GetValue, amount, balanceAfter, occurredAt);
        if (!recordResult.IsSuccess) return;

        HistStorage.Add(recordResult.GetValue);
    }
}
