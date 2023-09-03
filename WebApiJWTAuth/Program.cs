using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebApiJWTAuth;
using WebApiJWTAuth.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<JwtService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
});

builder.Services.AddAuthorization(x=>
{
    x.AddPolicy("PremiumPolicy", 
        p => p.RequireRole("Batman"));
});


WebApplication app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet(pattern: "/", handler: (JwtService service) 
    => service.Create(new WebApiJWTAuth.Models.User(
        1,
        "Wendel",
        "wendelviegas@gmail.com", 
        "1234",
        "1234567",
        new [] 
        {
            "Student",
            "Premium"
        })));

app.MapGet("/restrito", (ClaimsPrincipal user) => $"Autenticou com o usuario : {user.Identity?.Name}")
    .RequireAuthorization();

app.MapGet("/premium", (ClaimsPrincipal user) => $"Autenticou com o usuario : {user.Identity?.Name}")
    .RequireAuthorization("PremiumPolicy");


app.Run();

// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();


