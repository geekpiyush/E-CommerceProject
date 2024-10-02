using Entities.DatabaseContext;
using Entities.Identity;
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

builder.Services.AddIdentity<ApplicationUser,ApplicationUserRole>()
    
    .AddEntityFrameworkStores<ApplicationDbContext>()
   
    .AddDefaultTokenProviders()

    .AddUserStore<UserStore<ApplicationUser,ApplicationUserRole,ApplicationDbContext,Guid>>()
    
    .AddRoleStore<RoleStore<ApplicationUserRole,ApplicationDbContext,Guid>>();

var app = builder.Build();




app.UseStaticFiles();

app.UseAuthentication();//Read Cookies
app.UseRouting();
app.MapControllers(); // Execute Filter Pipeline


app.Run();
