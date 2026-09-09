namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.History;

public class HistoryResponse
{
    public IReadOnlyCollection<OperationHistoryEntry> OperationHistory { get; set; } = Array.Empty<OperationHistoryEntry>();
}