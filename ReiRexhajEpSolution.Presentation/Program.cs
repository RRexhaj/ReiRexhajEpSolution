using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReiRexhajEpSolution.DataAccess.Context;
using ReiRexhajEpSolution.DataAccess.Interfaces;
using ReiRexhajEpSolution.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core using the connection string from appsettings.json.
builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity to use the same PollDbContext.
// This sets up built-in login, registration, etc. pages.
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<PollDbContext>();

// Add Razor Pages so Identity’s default pages work.
builder.Services.AddRazorPages();

// Add MVC controllers with views (for custom pages such as Polls)
builder.Services.AddControllersWithViews();

// Register your Poll repository.
builder.Services.AddScoped<IPollRepository, PollRepositoryMain>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Enable Authentication and Authorization (Identity uses cookie authentication)
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages for Identity UI (ex: /Identity/Account/Login)
app.MapRazorPages();

// Map default controller route (adjust default controller if needed)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Poll}/{action=Index}/{id?}");

app.Run();
