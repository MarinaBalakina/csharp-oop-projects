namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.History;

public class OperationHistoryEntry
{
    public string OperationName { get; set; } = string.Empty;

    public decimal? Amount { get; set; }

    public decimal BalanceAfter { get; set; }

    public DateTimeOffset OccurredOn { get; set; }
}