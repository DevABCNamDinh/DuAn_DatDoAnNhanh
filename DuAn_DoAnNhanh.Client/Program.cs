﻿using DuAn_DoAnNhanh.Application.Implements.Repository;
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSingleton<IDesignTimeDbContextFactory<MyDBContext>, MyDbContextFactory>();

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IComboDetailsService, ComboDetailsService>();
builder.Services.AddScoped<IComboService, ComboSevice>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddHttpClient(); // Thêm dòng này để sử dụng HttpClient

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// C?u hình ?? truy c?p th? m?c Images trong DuAn_DoAnNhanh.Application
var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "DuAn_DoAnNhanh.Application", "Images");
// Kiểm tra nếu thư mục Images chưa tồn tại, thì tự động tạo
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/images"
});
app.UseStaticFiles();

app.UseSession();
app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
