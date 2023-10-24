using DataLayer;

namespace WebServer.Models;

public class ProductModel
{
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public CategoryModel? Category { get; set; }
}
