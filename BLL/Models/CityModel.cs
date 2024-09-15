#nullable disable

using BLL.DAL;

namespace BLL.Models
{
    public class CityModel
    {
        public City Record { get; set; }

        public string Name => Record.Name;
        public string Country => Record.Country?.Name;
    }
}
