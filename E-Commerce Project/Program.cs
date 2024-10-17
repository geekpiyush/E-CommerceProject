using Entities.DatabaseContext;
using Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories_;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

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

builder.Services.AddScoped<IProductCategoryAdderRepository,ProductCategoryAddRepository>();
builder.Services.AddScoped<IProductCategoryGetterRepository,ProductCategoryGetRepository>();
builder.Services.AddScoped<IProductDataAdderRepository, ProductDataAddRepository>();
builder.Services.AddScoped<IProductDataGetterRepository, ProductDataGetRepository>();
builder.Services.AddScoped<IProductDataUpdateRepository, ProductDataUpdateRepository>();
builder.Services.AddScoped<IProductDataDeleteRepository, ProductDataDeleteRepository>();

builder.Services.AddScoped<IProductCategoryGetterService ,ProductCategoryGetterService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductDataAddService , ProductDataAdderServie>();
builder.Services.AddScoped<IProductDataGetterService , ProductDataGetterService>();
builder.Services.AddScoped<IProductDataDeleteService , ProductDataDeleteService>();
builder.Services.AddScoped<IProductDataUpdateService , ProductDataUpdateService>();

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
