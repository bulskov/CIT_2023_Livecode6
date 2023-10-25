using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : BaseController
{
    private readonly IDataService _dataService;

    public ProductsController(IDataService dataService, LinkGenerator linkGenerator)
        :base(linkGenerator)
    {
        _dataService = dataService;
    }


    [HttpGet(Name = nameof(GetProducts))]
    public IActionResult GetProducts(int page = 0, int pageSize = 10)
    {
        (var products, var total) = _dataService.GetProducts(page, pageSize);

        var items = products.Select(CreateProductListModel);

        var result = Paging(items, total, page, pageSize, nameof(GetProducts));

        return Ok(result);
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

   
}
