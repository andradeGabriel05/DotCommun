using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.getUserById(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] User user)
    {
        var createUser = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = createUser.Id }, createUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, User user)
    {
        var updateUser = await _userService.UpdateAsync(id, user);
        return Ok(updateUser);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteUser = await _userService.DeleteAsync(id);
        return Ok(deleteUser);
    }
    
    
    
}