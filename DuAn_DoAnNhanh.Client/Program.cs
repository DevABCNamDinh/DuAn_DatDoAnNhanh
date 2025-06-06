﻿
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Implements.Service.JWT;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service.Jwt;
using DuAn_DoAnNhanh.Application.Share.Middlewares;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Implements.Repository;
using DuAn_DoAnNhanh.Data.Implements.UnitOfWork;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;
using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
using Elfie.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Client.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Bắt buộc cookie session hoạt động
});
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSingleton<IDesignTimeDbContextFactory<MyDBContext>, MyDbContextFactory>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IComboDetailsService, ComboDetailsService>();
builder.Services.AddScoped<IComboService, ComboSevice>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddHttpClient(); 
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Lấy các giá trị từ appsettings.json trực tiếp
        var secretKey = builder.Configuration["JwtSettings:SecretKey"];
        var expiresInMinutes = int.Parse(builder.Configuration["JwtSettings:ExpiresInMinutes"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,  // Không kiểm tra Issuer, nếu bạn cần có thể bật lên
            ValidateAudience = false,  // Không kiểm tra Audience, nếu bạn cần có thể bật lên
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero  // Không có độ trễ khi kiểm tra thời gian
        };
    });

var app = builder.Build();

//app.UseMiddleware<ExceptionHandlingMiddleware>();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
app.UseMiddleware<JwtSessionMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
