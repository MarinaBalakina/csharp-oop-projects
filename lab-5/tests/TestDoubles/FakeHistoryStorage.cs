using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests.TestDoubles;

public class FakeHistoryStorage : IHistoryStorage
{
    private readonly List<OperationRecord> _records;

    public FakeHistoryStorage()
    {
        _records = new List<OperationRecord>();
    }

    public IReadOnlyCollection<OperationRecord> Records => _records;

    public void Add(OperationRecord record)
    {
        _records.Add(record);
    }

    public IReadOnlyCollection<OperationRecord> GetByAccountNumber(AccountNumber accountNumber)
    {
        return _records
            .Where(record => record.Number.Equals(accountNumber))
            .ToArray();
    }
}