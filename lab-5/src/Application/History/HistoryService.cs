using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.History;

public class HistoryService
{
    private ISessionStorage SessionStorage { get; }

    private IHistoryStorage HistoryStorage { get; }

    public HistoryService(ISessionStorage sessionStorage, IHistoryStorage historyStorage)
    {
        SessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        HistoryStorage = historyStorage ?? throw new ArgumentNullException(nameof(historyStorage));
    }

    public OperationResultWithValue<IReadOnlyCollection<OperationRecord>> GetUserHistory(SessionKey userKey)
    {
        ISession? session = SessionStorage.FindByKey(userKey);
        if (session is not UserSession userSession)
            return OperationResultWithValue<IReadOnlyCollection<OperationRecord>>.Fail(OperationError.Unauthorized);

        AccountNumber accountNumber = userSession.AccNumber;

        IReadOnlyCollection<OperationRecord> recordResult = HistoryStorage.GetByAccountNumber(accountNumber);

        return OperationResultWithValue<IReadOnlyCollection<OperationRecord>>.Success(recordResult);
    }
}