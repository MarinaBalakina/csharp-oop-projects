using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;

namespace Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;

public interface IHistoryStorage
{
    void Add(OperationRecord record);

    IReadOnlyCollection<OperationRecord> GetByAccountNumber(AccountNumber accountNumber);
}