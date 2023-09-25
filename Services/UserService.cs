using ShopDH.Middlewares;
using ShopDH.Models;
using ShopDH.Repositories.Interfaces;

namespace ShopDH.Services;

public class UserService
{
    private readonly IUserRepos _userRepo;
    private readonly UserContext _userContext;

    public UserService(IUserRepos userRepos, UserContext userContext)
    {
        _userRepo = userRepos;
        _userContext = userContext;
    }
    public async Task<ResponseResult> GetAll()
    {
        var result = await _userRepo.GetAll();
        return new ResponseResult(true, "Thanh cong roi day", result);
    }

    public async Task<ResponseResult> GetById(long id)
    {
        var getAll = await _userRepo.GetAll();
        var getById = getAll.SingleOrDefault(x => x.Id == id);
        return new ResponseResult(true, "Thanh cong roi day", getById);
    }

    public Task<ResponseResult> GetUser()
    {
        var result = _userContext.Username;
        return Task.FromResult(new ResponseResult(true, "Thanh cong roi day", result));
    }

}