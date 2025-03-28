using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.EF
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDBContext>
    {
        public MyDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDBContext>();

            // Cấu hình chuỗi kết nối, thay "Your_Connection_String_Here" bằng chuỗi kết nối của bạn
            optionsBuilder.UseSqlServer("Server=DESKTOP-K3GKVBQ;Database=Do_An_Nhanh;Trusted_Connection=True;TrustServerCertificate=True");
            return new MyDBContext(optionsBuilder.Options);
        }
    }
}
