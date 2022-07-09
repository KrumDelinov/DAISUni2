using BusinessLogic.Services;
using BusinessLogic.Services.Contracts;
using DAISUni2;
using DataAccsess.DbContexts;
using DataAccsess.Services;
using DataAccsess.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("LocalDb");

builder.Services.AddDbContext<DAISUni2Context>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<ITeachersDA , TeachersDA>();
BaseStartup.ConfigureServices(builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
