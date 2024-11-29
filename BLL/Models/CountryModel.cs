using BLL.DAL;

namespace BLL.Models
{
    public class CountryModel
    {
        public Country Record { get; set; }

        public string Name => Record.Name;
    }
}
