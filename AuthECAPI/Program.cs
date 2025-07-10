using AuthECAPI.Controllers;
using AuthECAPI.Extensions;
using AuthECAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddIdentityAuth(builder.Configuration)
                .AddIdentityHandlersAndStores()
                .ConfigureIdentityOptions()
                .InjectDbContext(builder.Configuration)
                .AddAppConfig(builder.Configuration);







var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.ConfigureCORS(builder.Configuration)
   .AddIdentityAuthMiddlewares();

app.MapControllers();

//app.MapGroup("/api")
//    .MapIdentityApi<IdentityUser>();

//app.MapGroup("/api")
//    .MapIdentityApi<AppUser>();


//app.MapPost("/signup", async (
//    UserManager<AppUser> userManager,
//    [FromBody] UserRegistrationModel userRegistrationModel
//    ) =>
//{
//    AppUser user = new AppUser
//    {
//        UserName = userRegistrationModel.Email, // Identity requires UserName, so we use Email as UserName
//        Email = userRegistrationModel.Email,
//        FullName = userRegistrationModel.FullName
//    };
//    var result = await userManager.CreateAsync(
//        user,
//        userRegistrationModel.Password);

//    if (result.Succeeded)
//        return Results.Ok(new { message = "User created successfully." });
//    else
//        return Results.BadRequest(result);

//});
//// This UserManager class is from the Identity Manager layer which we saw


//var apiGroup = app.MapGroup("/api");
//apiGroup.MapIdentityApi<AppUser>();

app.MapGroup("/api")
    .MapIdentityApi<AppUser>();

app.MapGroup("/api")
   .MapIdentityUserEndpoints();


//apiGroup.MapPost("/signup", async (
//    UserManager<AppUser> userManager,
//    [FromBody] UserRegistrationModel userRegistrationModel
//    ) => {
//        AppUser user = new AppUser
//        {
//            UserName = userRegistrationModel.Email,
//            Email = userRegistrationModel.Email,
//            FullName = userRegistrationModel.FullName
//        };
//        var result = await userManager.CreateAsync(user, userRegistrationModel.Password);

//        if (result.Succeeded)
//            return Results.Ok(new { message = "User created successfully." });
//        else
//            return Results.BadRequest(result);
//    });


//apiGroup.MapPost("/signin", async (
//    UserManager<AppUser> userManager,
//    [FromBody] LoginModel loginModel) =>
//{
//    var user = await userManager.FindByEmailAsync(loginModel.Email);
//    if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
//    {
//        var signInKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JWTSecret"]!));
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {   // this claim constructor expects an array of claims but right now we have only one claim
//            Subject = new ClaimsIdentity(new Claim[]
//            {
//                    new Claim("UserID", user.Id.ToString())
//            }),
//            Expires = DateTime.UtcNow.AddDays(7), // token will be valid for 7 days
//            SigningCredentials = new SigningCredentials(
//                signInKey,
//                SecurityAlgorithms.HmacSha256Signature
//                )
//        };
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
//        var token = tokenHandler.WriteToken(securityToken);
//        return Results.Ok(new
//        {
//            token = token,
//            userId = user.Id,
//            fullName = user.FullName
//        });
//    }
//    else
//        return Results.BadRequest(new { message = "Username or Password is incorrect." });
//});



app.Run();