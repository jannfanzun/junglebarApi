namespace junglebarApi.Controllers;

using junglebarApi.Models;
using junglebarApi.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }
    
    [HttpGet("GetUserByEmailAndPassword")]
    public ActionResult<User> GetUserByEmailAndPassword([FromQuery] string email, [FromQuery] string password)
    {
        var user = _userService.GetUserByEmailAndPassword(email, password);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult AddUser([FromBody] User user)
    {
        _userService.AddUser(user);
        return Ok();
    }
}
