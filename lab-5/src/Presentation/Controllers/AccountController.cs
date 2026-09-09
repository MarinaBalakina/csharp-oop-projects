using Itmo.ObjectOrientedProgramming.Lab5.Application.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Results;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Sessions;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Contracts.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private AccountService CurAccountService { get; }

    private IOperationErrorToHttp ErrorMapper { get; }

    public AccountController(AccountService accountService, IOperationErrorToHttp errorMapper)
    {
        CurAccountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        ErrorMapper = errorMapper ?? throw new ArgumentNullException(nameof(errorMapper));
    }

    [HttpPost("create")]
    public IActionResult CreateAccount([FromBody] CreateAccountRequest request)
    {
        OperationResultWithValue<SessionKey> keyResult = SessionKey.FromGuid(request.AdminSessionKey);
        if (!keyResult.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(keyResult.Error);
            return StatusCode(statusCode, keyResult.Error.ToString());
        }

        OperationResult result = CurAccountService.CreateAccount(
            keyResult.GetValue,
            request.AccountNumber,
            request.PinCode,
            request.InitialBalance);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        return Ok();
    }

    [HttpPost("balance")]
    public IActionResult GetBalance([FromBody] BalanceRequest request)
    {
        OperationResultWithValue<SessionKey> keyResult = SessionKey.FromGuid(request.UserSessionKey);
        if (!keyResult.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(keyResult.Error);
            return StatusCode(statusCode, keyResult.Error.ToString());
        }

        OperationResultWithValue<MoneyAmount> result = CurAccountService.GetBalance(keyResult.GetValue);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        var response = new BalanceResponse { Balance = result.GetValue.Value };

        return Ok(response);
    }

    [HttpPost("deposit")]
    public IActionResult Deposit([FromBody] DepositRequest request)
    {
        OperationResultWithValue<SessionKey> keyResult = SessionKey.FromGuid(request.UserSessionKey);
        if (!keyResult.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(keyResult.Error);
            return StatusCode(statusCode, keyResult.Error.ToString());
        }

        OperationResult result = CurAccountService.Deposit(keyResult.GetValue, request.Amount);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        return Ok();
    }

    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] WithdrawRequest request)
    {
        OperationResultWithValue<SessionKey> keyResult = SessionKey.FromGuid(request.UserSessionKey);
        if (!keyResult.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(keyResult.Error);
            return StatusCode(statusCode, keyResult.Error.ToString());
        }

        OperationResult result = CurAccountService.Withdraw(keyResult.GetValue, request.Amount);

        if (!result.IsSuccess)
        {
            int statusCode = ErrorMapper.ToStatusCode(result.Error);
            return StatusCode(statusCode, result.Error.ToString());
        }

        return Ok();
    }
}