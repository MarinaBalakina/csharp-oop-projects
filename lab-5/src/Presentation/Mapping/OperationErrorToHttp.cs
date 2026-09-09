using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Mapping;

public class OperationErrorToHttp : IOperationErrorToHttp
{
    private IReadOnlyDictionary<OperationError, int> Mapping { get; }

    private int DefaultStatusCode { get; }

    public OperationErrorToHttp(IReadOnlyDictionary<OperationError, int> mapping, int defaultStatusCode)
    {
        Mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));

        DefaultStatusCode = defaultStatusCode;
    }

    public int ToStatusCode(OperationError operationError)
    {
        return Mapping.TryGetValue(operationError, out int statusCode) ? statusCode : DefaultStatusCode;
    }
}