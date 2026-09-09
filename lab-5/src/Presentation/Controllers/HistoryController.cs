using Itmo.ObjectOrientedProgramming.Lab5.Application.History;
using Itmo.ObjectOrientedProgramming.Lab5.Core.History;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.History;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Controllers;

[ApiController]
public class HistoryController : ControllerBase
{
    private HistoryService CurHistoryService { get; }

    private IOperationErrorToHttp ErrorMapper { get; }

    public HistoryController(HistoryService historyService, IOperationErrorToHttp errorMapper)
    {
        CurHistoryService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        ErrorMapper = errorMapper ?? throw new ArgumentNullException(nameof(errorMapper));
    }

    [HttpPost("history")]
    public IActionResult GetHistory([FromBody] HistoryRequest request)
    {
        OperationResultWithValue<SessionKey> keyResult = SessionKey.FromGuid(request.UserSessionKey);
        if (!keyResult.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(keyResult.Error);
            return StatusCode(statusCode, keyResult.Error.ToString());
        }

        OperationResultWithValue<IReadOnlyCollection<OperationRecord>> result =
            CurHistoryService.GetUserHistory(keyResult.GetValue);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        OperationHistoryEntry[] entries = result.GetValue
            .Select(static record => new OperationHistoryEntry
            {
                OperationName = record.Name.Value,
                Amount = record.Amount?.Value,
                BalanceAfter = record.BalanceAfter.Value,
                OccurredOn = record.OccurredAt,
            })
            .ToArray();

        var response = new HistoryResponse
        {
            OperationHistory = entries,
        };

        return Ok(response);
    }
}