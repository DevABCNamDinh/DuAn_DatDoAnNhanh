using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        ClaimsPrincipal ValidateToken(string token);
    }
}
