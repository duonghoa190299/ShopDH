using Microsoft.AspNetCore.Mvc;
using ShopDH.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ShopDH.Services;

namespace ShopDH.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;


    public UserController(UserService userService)
    {
        _userService = userService;

    }
    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _userService.GetAll());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult(false, ex.Message + ex.StackTrace, null));
        }
    }

    [HttpGet(nameof(GetById))]
    public async Task<IActionResult> GetById(long id, int x)
    {
        try
        {
            return Ok(await _userService.GetById(id));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult(false, ex.Message + ex.StackTrace, null));
        }
    }

    [HttpGet(nameof(GetUsers))]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            return Ok(await _userService.GetUser());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult(false, ex.Message + ex.StackTrace, null));
        }
    }

}
