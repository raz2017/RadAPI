using Microsoft.AspNetCore.Mvc;
using RadancyAPI.Controllers.Exceptions;
using RadancyAPI.Controllers.Inputs;
using RadancyAPI.Domain;
using RadancyAPI.Services;

namespace RadancyAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult<Account> Get([FromQuery] Guid userId, [FromQuery] Guid accountId)
    {
        var account = _accountService.GetAccount(userId, accountId);

        return Ok(account);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult<Account> Create([FromBody] CreateAccountInput input)
    {
        var account = _accountService.CreateAccount(input.UserId);

        return Ok(account);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult<Account> Update([FromBody] UpdateAccountInput input)
    {
        var account = _accountService.UpdateAccount(input.UserId,
            input.AccountId,
            input.AccountAction,
            input.Amount);

        return Ok(account);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult Delete([FromQuery] Guid userId, [FromQuery] Guid accountId)
    {
        _accountService.RemoveAccount(userId, accountId);

        return NoContent();
    }
}