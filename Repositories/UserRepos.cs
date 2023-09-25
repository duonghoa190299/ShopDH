using ShopDH.Context;
using ShopDH.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using ShopDH.Repositories.Interfaces;
using ShopDH.Models;

namespace ShopDH.Repositories;

public class UserRepos : IUserRepos
{
    private readonly EFContext eFContext;


    public UserRepos(EFContext _eFContext)
    {
        eFContext = _eFContext;
    }

    public async Task<IEnumerable<Users>> GetAll()
    {
        return await eFContext.Users.ToListAsync();
    }

    public async Task<Users?> GetByUsername(string username)
    {
        if (username == null)
        {
            throw new ArgumentNullException();
        }

        var user = await eFContext.Users.SingleOrDefaultAsync(x => x.Username == username);
        return user;
    }

    public async Task<Users> GetByEmail(string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException();
        }

        var user = await (from u in eFContext.Users
                          where u.Email == email
                          select u).FirstOrDefaultAsync<Users>();
        return user!;
    }

    public async Task<bool> Insert(Users user)
    {
        user.Modified = DateTime.Now;
        user.Status = 0;

        await eFContext.Users.AddAsync(user);

        return await eFContext.SaveChangesAsync() > 0;
    }
}