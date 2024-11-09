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
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6F71DIH\\SQLEXPRESS;Initial Catalog=Do_An_Nhanh;Integrated Security=True;TrustServerCertificate=true");

            return new MyDBContext(optionsBuilder.Options);
        }
    }
}
