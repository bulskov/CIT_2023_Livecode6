using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoriesController : BaseController
{
    private readonly IDataService _dataService;

    public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
        :base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetCetagories))]
    public IActionResult GetCetagories(int page = 0, int pageSize = 10)
    {
       (var categories, var total) = _dataService.GetCategories(page, pageSize);

        var items = categories.Select(CreateCategoryModel);

        var result = Paging(items, total, page, pageSize, nameof(GetCetagories));

        return Ok(result);
    }
    
    [HttpGet("{id}", Name = nameof(GetCategory))]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);
        if(category == null)
        {
            return NotFound();
        }

        return Ok(CreateCategoryModel(category));
    }

    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryModel model)
    {
        var category = new Category
        {
            Name = model.Name,
            Description = model.Description
        };

        _dataService.CreateCategory(category);

        return Ok(category);
    }


    private CategoryModel CreateCategoryModel(Category category)
    {
        return new CategoryModel
        {
            Url = GetUrl(nameof(GetCategory), new { category.Id }),
            Name = category.Name,
            Description = category.Description
        };
    }

}
