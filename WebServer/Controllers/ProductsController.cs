using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductsController(IDataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }


    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _dataService.GetProducts()
                            .Select(CreateProductListModel);
        return Ok(products);
    }

    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if(product == null)
        {
            return NotFound();
        }
        return Ok(CreateProductModel(product));
    }

    private ProductModel CreateProductModel(Product product)
    {
        return new ProductModel
        {
            Url = GetUrl(nameof(GetProduct), new { product.Id }),
            Name = product.Name,
            Price = product.Price,
            Category = new CategoryModel
            {
                Url = GetCategoryUrl(product.CategoryId),
                Name = product.Category?.Name ?? "Not defined",
                Description = product.Category?.Description
            }
        };
    }

    private ProductListModel CreateProductListModel(Product product)
    {
        return new ProductListModel
        {
            Url = GetUrl(nameof(GetProduct), new { product.Id }),
            Name = product.Name,
            CategoryName = product.Category?.Name
        };
    }

    private string GetCategoryUrl(int id)
    {
        return GetUrl(nameof(CategoriesController.GetCategory), new { id });
    }

    private string GetUrl(string name, object values)
    {
        return _linkGenerator.GetUriByName(HttpContext, name, values) ?? "Not specified";
    }
}
