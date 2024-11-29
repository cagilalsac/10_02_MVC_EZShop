using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class City
{
    public int Id { get; set; }

    [Required]
    [StringLength(125)]
    public string Name { get; set; }

    [Required]
    public int? CountryId { get; set; }

    public Country Country { get; set; }

    public List<Store> Stores { get; set; } = new List<Store>();
}