using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopDH.Middlewares;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.Configure<Appsettings>(configuration);
builder.Services.AddDbContext<EFContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<EFContext>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenHelpers>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepos, UserRepos>();
builder.Services.AddSingleton<UserContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWTConfiguration:Audience"],
        ValidIssuer = configuration["JWTConfiguration:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfiguration:Key"] ?? string.Empty))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<JWTMiddleware>();

app.Run();
