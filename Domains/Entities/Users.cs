using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDH.Domains.Entities;

[Table(nameof(Users))]
public class Users
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
    public string? Fullname { get; set; }
    public string? Gender { get; set; }
    public string? Avatar { get; set; }
    public DateTime? Birthday { get; set; }
    public bool? IsAdmin { get; set; }
    public int Status { get; set; }
    public DateTime Modified { get; set; }
    public int? ModifiedBy { get; set; }
}