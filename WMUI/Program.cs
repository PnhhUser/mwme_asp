
using Application.Extensions;
using Data.Context;
using Data.Extensions;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var server = Environment.GetEnvironmentVariable("DB_SERVER")!;
var dbName = Environment.GetEnvironmentVariable("DB_NAME")!;
var user = Environment.GetEnvironmentVariable("DB_USER")!;
var password = Environment.GetEnvironmentVariable("DB_PASSWORD")!;

var connStr = $"Server={server};Database={dbName};User={user};Password={password};";
var serverVersion = ServerVersion.AutoDetect(connStr);

builder.Services.AddDbContext<MWMeDbContext>(options =>
{
    options.UseMySql(connStr, serverVersion);
});

builder.Services
.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.Cookie.Name = "mwme";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
