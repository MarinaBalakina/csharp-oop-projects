using Itmo.ObjectOrientedProgramming.Lab5.Application.Sessions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.Sessions;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Controllers;

[ApiController]
public class SessionController : ControllerBase
{
    private SessionService CurSessionService { get; }

    private IOperationErrorToHttp ErrorMapper { get; }

    public SessionController(SessionService sessionService, IOperationErrorToHttp errorMapper)
    {
        CurSessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
        ErrorMapper = errorMapper ?? throw new ArgumentNullException(nameof(errorMapper));
    }

    [HttpPost("user")]
    public IActionResult CreateUserSession([FromBody] UserSessionRequest request)
    {
        OperationResultWithValue<SessionKey> result =
            CurSessionService.CreateUserSession(request.AccountNumber, request.PinCode);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        var response = new SessionKeyResponse { SessionKey = result.GetValue.Value };
        return Ok(response);
    }

    [HttpPost("admin")]
    public IActionResult CreateAdminSession([FromBody] AdminSessionRequest request)
    {
        OperationResultWithValue<SessionKey> result = CurSessionService.CreateAdminSession(request.SystemPassword);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        var response = new SessionKeyResponse { SessionKey = result.GetValue.Value };
        return Ok(response);
    }
}