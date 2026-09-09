using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Mapping;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.DependencyInjection;

public static class WebApiServiceExtensions
{
    public static IServiceCollection AddErrorMapping(this IServiceCollection services)
    {
        var mapping = new Dictionary<OperationError, int>
        {
            [OperationError.Unauthorized] = StatusCodes.Status401Unauthorized,
            [OperationError.InvalidSessionKey] = StatusCodes.Status400BadRequest,
            [OperationError.InvalidAccountNumber] = StatusCodes.Status400BadRequest,
            [OperationError.InvalidPinCode] = StatusCodes.Status400BadRequest,
            [OperationError.AmountMustBePositive] = StatusCodes.Status400BadRequest,
            [OperationError.InsufficientFunds] = StatusCodes.Status400BadRequest,
        };

        services.AddSingleton<IOperationErrorToHttp>(_ =>
            new OperationErrorToHttp(mapping, StatusCodes.Status400BadRequest));

        return services;
    }
}