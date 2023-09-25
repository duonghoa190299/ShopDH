namespace ShopDH;

public class Appsettings
{
    public JWTConfiguration JWTConfiguration { get; set; } = null!;
}

public class JWTConfiguration
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Subject { get; set; } = null!;
}