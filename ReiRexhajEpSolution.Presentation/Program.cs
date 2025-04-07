using Microsoft.EntityFrameworkCore;
using ReiRexhajEpSolution.DataAccess.Context;
using ReiRexhajEpSolution.DataAccess.Repositories;
using ReiRexhajEpSolution.DataAccess.Interfaces;
using ReiRexhajEpSolution.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Configure EF Core (using In-Memory for demo purposes)
builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseInMemoryDatabase("PollDb"));

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
