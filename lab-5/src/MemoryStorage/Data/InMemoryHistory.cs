using Itmo.ObjectOrientedProgramming.Lab5.Abstractions.DataAccess;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;

namespace Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Data;

public class InMemoryHistory : IHistoryStorage
{
    private List<OperationRecord> Records { get; }

    public InMemoryHistory()
    {
        Records = new List<OperationRecord>();
    }

    public void Add(OperationRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        Records.Add(record);
    }

    public IReadOnlyCollection<OperationRecord> GetByAccountNumber(AccountNumber accountNumber)
    {
        ArgumentNullException.ThrowIfNull(accountNumber);

        string key = accountNumber.Value;

        OperationRecord[] recordResult = Records.Where(record => record.Number.Value == key).ToArray();

        return recordResult;
    }
}