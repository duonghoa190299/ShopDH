using ShopDH.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShopDH.Repositories.Interfaces;
public interface IUserRepos
{
    Task<IEnumerable<Users>> GetAll();
    Task<Users?> GetByUsername(string username);
    Task<Users?> GetByEmail(string email);
    Task<bool> Insert(Users users);

}