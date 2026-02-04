using GameFrontEnd.Mvc.Service;
using GameManager.Core.Domain.IdentityEntities;
using GameManager.Core.ServiceContract;
using GameManager.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string baseUrl = "http://localhost:5140/";
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IGameService, GameService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

//Enable Identity in project
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole,ApplicationDbContext,Guid>>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
