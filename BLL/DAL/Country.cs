using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class Country
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public List<City> Cities { get; set; } = new List<City>();

    public List<Store> Stores { get; set; } = new List<Store>();
}