namespace ThreeLayerArchitecture.Models;

/// <summary>
/// Domain model - представляє продукт в системі
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
}
