using DuAn_DoAnNhanh.Application.Implements.Repository;
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<MyDBContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
builder.Services.AddDbContext<MyDBContext>((serviceProvider, options) =>
{
    var dbContextFactory = serviceProvider.GetRequiredService<IDesignTimeDbContextFactory<MyDBContext>>();
    options.UseSqlServer("Data Source=DESKTOP-6F71DIH\\SQLEXPRESS;Initial Catalog=Do_An_Nhanh;Integrated Security=True;TrustServerCertificate=true");
});
builder.Services.AddSingleton<IDesignTimeDbContextFactory<MyDBContext>, MyDbContextFactory>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();

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
