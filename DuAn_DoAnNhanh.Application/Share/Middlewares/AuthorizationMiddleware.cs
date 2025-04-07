using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Share.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            // Kiểm tra nếu lỗi 403 (Không có quyền)
            if (context.Response.StatusCode == 403 || context.Response.StatusCode == 401)
            {
                // chuyển hướng đến trang thông báo
                context.Response.Redirect("/User/Login");
            }
        }
    }
}
