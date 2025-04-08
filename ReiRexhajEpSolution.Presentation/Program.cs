using Microsoft.EntityFrameworkCore;
using ReiRexhajEpSolution.DataAccess.Context;
using ReiRexhajEpSolution.DataAccess.Repositories;
using ReiRexhajEpSolution.DataAccess.Interfaces;
using ReiRexhajEpSolution.Presentation.Filters;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Configure EF Core (using SQL Server)
builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PollDbContext>()
    .AddDefaultTokenProviders();

// Register repositories and filters
builder.Services.AddScoped<IPollRepository, PollRepositoryMain>();
builder.Services.AddScoped<OneVotePerPollFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Poll}/{action=Index}/{id?}");

app.Run();
