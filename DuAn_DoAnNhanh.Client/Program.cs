using DuAn_DoAnNhanh.Application.Implements.Repository;
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddDbContext<MyDBContext>((serviceProvider, options) =>
{
    var dbContextFactory = serviceProvider.GetRequiredService<IDesignTimeDbContextFactory<MyDBContext>>();
    options.UseSqlServer("Data Source=DESKTOP-PMMSQG0\\SQLEXPRESS;Initial Catalog=Du_An;Integrated Security=True;TrustServerCertificate=true");
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
