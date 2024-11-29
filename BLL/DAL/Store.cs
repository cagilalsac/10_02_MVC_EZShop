using System.ComponentModel.DataAnnotations;

namespace BLL.DAL;

public class Store
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    public bool IsVirtual { get; set; }

    public int? CountryId { get; set; }

    public int? CityId { get; set; }
    
    public City City { get; set; }
    
    public Country Country { get; set; }
    
    public List<ProductStore> ProductStores { get; set; } = new List<ProductStore>();
}