using Microsoft.AspNetCore.Mvc;
using RadancyAPI.Controllers.Exceptions;
using RadancyAPI.Controllers.Inputs;
using RadancyAPI.Domain;
using RadancyAPI.Services;

namespace RadancyAPI.Controllers;

/// <summary>
/// In order to create an account you must first create the user.
/// These endpoints allow you to create, update, and delete user details.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult<User> Get(Guid id)
    {
        return Ok(_userService.GetUser(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult<User> Create([FromBody] UserInput input)
    {
        var user = _userService.AddUser(new User(input.FirstName,
            input.LastName,
            input.Email,
            input.DOB));

        return Ok(user);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult Update(Guid id, [FromBody] UserInput input)
    {
        _userService.Update(new User(input.FirstName,
            input.LastName,
            input.Email,
            input.DOB,
            id));

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(HttpResponseException), StatusCodes.Status400BadRequest)]
    public ActionResult Delete(Guid id)
    { 
        _userService.RemoveUser(id);

        return NoContent();
    }
}