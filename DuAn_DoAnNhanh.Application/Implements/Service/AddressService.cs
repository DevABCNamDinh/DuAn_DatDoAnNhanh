using DuAn_DoAnNhanh.Application.Interfaces.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class AddressService : IAddressService
    {
        private readonly string _apiKey = "GTzwweyhgu0GBvSH0XJjPkPDwYeFkVV_ok80Oyas_qA";
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public (double Latitude, double Longitude) GetCoordinates(string address)
        {
            string url = $"https://geocode.search.hereapi.com/v1/geocode?q={address}&apiKey={_apiKey}";

            var response = _httpClient.GetAsync(url).Result; // Chuyển thành đồng bộ
            if (!response.IsSuccessStatusCode)
                return (0.0, 0.0);

            var result = response.Content.ReadAsStringAsync().Result; // Chuyển thành đồng bộ

            // Parse kết quả JSON trả về từ Here API để lấy tọa độ
            // Giả sử kết quả trả về có dạng:
            // {"items":[{"position":{"lat":21.0285,"lng":105.8542}}]}
            var coordinates = JsonConvert.DeserializeObject<dynamic>(result);
            if (coordinates.items != null && coordinates.items.Count > 0)
            {
                double latitude = coordinates.items[0].position.lat;
                double longitude = coordinates.items[0].position.lng;
                return (latitude, longitude);
            }

            return (0.0, 0.0);
        }
    }
}
