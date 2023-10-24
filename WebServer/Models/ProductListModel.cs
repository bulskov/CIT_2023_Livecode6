using DataLayer;

namespace WebServer.Models;

public class ProductListModel
{
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
}
