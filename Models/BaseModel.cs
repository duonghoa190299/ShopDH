using System.ComponentModel.DataAnnotations;

namespace ShopDH.Models;

public class BaseModel

{
    //[Key]
    public int Id { get; set; }
    public int Status { get; set; }
    public DateTime Modified { get; set; }
    public int ModifiedBy { get; set; }
}