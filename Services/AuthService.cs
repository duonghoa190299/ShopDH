using System.Security.Claims;

namespace ShopDH.Services;
public class AuthService
{

    private readonly IUserRepos _userRepos;
    private readonly TokenHelpers _tokenHelpers;

    public AuthService(IUserRepos userRepos, TokenHelpers tokenHelpers)
    {
        _userRepos = userRepos;
        _tokenHelpers = tokenHelpers;
    }

    public async Task<ResponseResult> SignIn(SignInRequest signInRequest)
    {
        var user = await _userRepos.GetByUsername(signInRequest.Username);
        if (user == null)
        {
            return new ResponseResult(false, "User not existed!", null);
        }
        else if (BCrypt.Net.BCrypt.Verify(signInRequest.Password, user.Password))
        {
            var claims = new Claim[]
            {
                new ("Username", user.Username),
                new ("Email", user.Email),
                new ("Id", user.Id.ToString())
            };

            var token = _tokenHelpers.GenarateAccessToken(claims);
            return new ResponseResult(true, "thanh cong", new { token });
        }
        else return new ResponseResult(false, "incorrect password!", null);
    }

    public async Task<ResponseResult> SignUp(SignUpRequest request)
    {
        var isUserExist = await _userRepos.GetByUsername(request.Username) != null;
        var isEmailExist = await _userRepos.GetByEmail(request.Email) != null;

        if (isUserExist)
        {
            return new ResponseResult(false, "User existed!", null);
        }
        else if (isEmailExist)
        {
            return new ResponseResult(false, "Email existed!", null);
        }
        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        Users user = new()
        {
            Username = request.Username,
            Password = request.Password,
            Email = request.Email
        };
        var createdUser = await _userRepos.Insert(user);
        return createdUser
                    ? new ResponseResult(true, "Thanh cong roi day", null)
                    : new ResponseResult(false, "That bai", null);
    }
}