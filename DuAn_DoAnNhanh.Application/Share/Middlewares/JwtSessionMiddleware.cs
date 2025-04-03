using DuAn_DoAnNhanh.Application.Interfaces.Service.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Share.Middlewares
{
    public class JwtSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtService jwtService)
        {
            var token = context.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var principal = jwtService.ValidateToken(token);
                    context.User = principal;
                }
                catch
                {
                    // Token không hợp lệ hoặc đã hết hạn
                    context.Session.Remove("AuthToken");
                }
            }

            await _next(context);
        }
    }
}
