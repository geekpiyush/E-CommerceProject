using Entities.DatabaseContext;
using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationUserRole>(options =>
{
    options.Password.RequiredLength = 5; //Required Length
    options.Password.RequireNonAlphanumeric = false; // symbols
    options.Password.RequireUppercase = false; //Uppercase char
    options.Password.RequireLowercase = true; //Lowercase char
    options.Password.RequireDigit = false; // Number
    options.Password.RequiredUniqueChars = 2; // Unique Char in Password
})
    
    .AddEntityFrameworkStores<ApplicationDbContext>()
   
    .AddDefaultTokenProviders()

    .AddUserStore<UserStore<ApplicationUser,ApplicationUserRole,ApplicationDbContext,Guid>>()
    
    .AddRoleStore<RoleStore<ApplicationUserRole,ApplicationDbContext,Guid>>();


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();




app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//Read Cookies
app.UseAuthorization(); //Authorize access permissions 
app.MapControllers(); // Execute Filter Pipeline


app.Run();
