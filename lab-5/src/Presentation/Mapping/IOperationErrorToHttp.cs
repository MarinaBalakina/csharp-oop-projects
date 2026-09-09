using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Mapping;

public interface IOperationErrorToHttp
{
    int ToStatusCode(OperationError operationError);
}