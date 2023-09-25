using Microsoft.AspNetCore.Mvc;
using ShopDH.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using ShopDH.Services;

namespace ShopDH.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthControllers : ControllerBase
{
    private readonly AuthService _authService;

    public AuthControllers(AuthService authService)
    {
        _authService = authService;

    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInRequest signInRequest)
    {
        try
        {
            return Ok(await _authService.SignIn(signInRequest));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult(false, ex.Message + ex.StackTrace, null));
        }
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
    {
        try
        {
            return Ok(await _authService.SignUp(signUpRequest));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult(false, ex.Message + ex.StackTrace, null));
        }
    }

}
