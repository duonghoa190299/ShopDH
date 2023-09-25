using Microsoft.EntityFrameworkCore;
using ShopDH.Domains.Entities;

namespace ShopDH.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        { }
        public DbSet<Users> Users { get; set; } = null!;
    }
}