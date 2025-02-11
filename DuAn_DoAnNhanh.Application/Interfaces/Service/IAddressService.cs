using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IAddressService
    {
        (double Latitude, double Longitude) GetCoordinates(string address);
    }
}
