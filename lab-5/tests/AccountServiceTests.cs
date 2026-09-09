using Itmo.ObjectOrientedProgramming.Lab5.Application.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;
using Itmo.ObjectOrientedProgramming.Lab5.Tests.TestDoubles;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class AccountServiceTests
{
    [Fact]
    public void Deposit_UpdatesBalanceAndHistory()
    {
        var accountStorage = new FakeAccountStorage();
        var sessionStorage = new FakeSessionStorage();
        var historyStorage = new FakeHistoryStorage();
        var clock = new FakeClock();
        var accountFactory = new FakeAccountFactory();

        var sessionKey = SessionKey.CreateNew();

        OperationResultWithValue<AccountNumber> numberResult = AccountNumber.Create("123456");
        OperationResultWithValue<PinCode> pinResult = PinCode.Create("0000");
        OperationResultWithValue<MoneyAmount> balanceResult = MoneyAmount.Create(100m);

        var account = new Account(
            numberResult.GetValue,
            pinResult.GetValue,
            balanceResult.GetValue);

        accountStorage.Add(account);

        var userSession = new UserSession(sessionKey, account.Number);
        sessionStorage.Add(userSession);

        var service = new AccountService(
            accountStorage,
            sessionStorage,
            historyStorage,
            accountFactory,
            clock);

        decimal depositAmount = 50m;

        OperationResult result = service.Deposit(sessionKey, depositAmount);

        Assert.True(result.IsSuccess);

        Account? storedAccount = accountStorage.FindByNumber(account.Number);
        Account notNullAccount = Assert.IsType<Account>(storedAccount);
        Assert.Equal(150m, notNullAccount.ViewBalance.Value);

        Assert.Single(historyStorage.Records);
        OperationRecord historyRecord = historyStorage.Records.First();
        Assert.Equal(account.Number, historyRecord.Number);
        Assert.Equal(150m, historyRecord.BalanceAfter.Value);
    }

    [Fact]
    public void Withdraw_WhenBalanceEnough_UpdatesBalanceAndHistory()
    {
        var accountStorage = new FakeAccountStorage();
        var sessionStorage = new FakeSessionStorage();
        var historyStorage = new FakeHistoryStorage();
        var clock = new FakeClock();
        var accountFactory = new FakeAccountFactory();

        var sessionKey = SessionKey.CreateNew();

        OperationResultWithValue<AccountNumber> numberResult = AccountNumber.Create("123456");
        OperationResultWithValue<PinCode> pinResult = PinCode.Create("0000");
        OperationResultWithValue<MoneyAmount> balanceResult = MoneyAmount.Create(100m);

        var account = new Account(
            numberResult.GetValue,
            pinResult.GetValue,
            balanceResult.GetValue);

        accountStorage.Add(account);

        var userSession = new UserSession(sessionKey, account.Number);
        sessionStorage.Add(userSession);

        var service = new AccountService(
            accountStorage,
            sessionStorage,
            historyStorage,
            accountFactory,
            clock);

        decimal withdrawAmount = 40m;

        OperationResult result = service.Withdraw(sessionKey, withdrawAmount);

        Assert.True(result.IsSuccess);

        Account? storedAccount = accountStorage.FindByNumber(account.Number);
        Account notNullAccount = Assert.IsType<Account>(storedAccount);
        Assert.Equal(60m, notNullAccount.ViewBalance.Value);

        Assert.Single(historyStorage.Records);
        OperationRecord historyRecord = historyStorage.Records.First();
        Assert.Equal(account.Number, historyRecord.Number);
        Assert.Equal(60m, historyRecord.BalanceAfter.Value);
    }

    [Fact]
    public void Withdraw_WhenBalanceNotEnough_ReturnsErrorAndKeepsBalance()
    {
        var accountStorage = new FakeAccountStorage();
        var sessionStorage = new FakeSessionStorage();
        var historyStorage = new FakeHistoryStorage();
        var clock = new FakeClock();
        var accountFactory = new FakeAccountFactory();

        var sessionKey = SessionKey.CreateNew();

        OperationResultWithValue<AccountNumber> numberResult = AccountNumber.Create("123456");
        OperationResultWithValue<PinCode> pinResult = PinCode.Create("0000");
        OperationResultWithValue<MoneyAmount> balanceResult = MoneyAmount.Create(100m);

        var account = new Account(
            numberResult.GetValue,
            pinResult.GetValue,
            balanceResult.GetValue);

        accountStorage.Add(account);

        var userSession = new UserSession(sessionKey, account.Number);
        sessionStorage.Add(userSession);

        var service = new AccountService(
            accountStorage,
            sessionStorage,
            historyStorage,
            accountFactory,
            clock);

        decimal withdrawAmount = 200m;

        OperationResult result = service.Withdraw(sessionKey, withdrawAmount);

        Assert.False(result.IsSuccess);
        Assert.Equal(OperationError.InsufficientFunds, result.Error);

        Account? storedAccount = accountStorage.FindByNumber(account.Number);
        Account notNullAccount = Assert.IsType<Account>(storedAccount);
        Assert.Equal(100m, notNullAccount.ViewBalance.Value);

        Assert.Empty(historyStorage.Records);
    }
}
