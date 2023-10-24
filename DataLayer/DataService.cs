using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DataLayer;

public class DataService : IDataService
{
    
    public void CreateCategory(Category category)
    {
        var db = new NorthwindContex();
        var id = db.Categories.Max(x => x.Id) + 1;
        var newCategory = new Category
        {
            Id = id,
            Name = category.Name,
            Description = category.Description
        };
        db.Add(category);
        db.SaveChanges();
    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContex();
        var category = db.Categories.FirstOrDefault(x => x.Id == id);
        if (category != null)
        {
            db.Categories.Remove(category);
            return db.SaveChanges() > 0;
        }
        return false;
    }

    public IList<Category> GetCategories()
    {
        var db = new NorthwindContex();
        return db.Categories.ToList();
    }

    public IList<Category> GetCategoriesByName(string name)
    {
        var db = new NorthwindContex();
        return db.Categories.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
    }

    public Category? GetCategory(int id)
    {
        var db = new NorthwindContex();
        return db.Categories.FirstOrDefault(x => x.Id == id);
    }

    public Product? GetProduct(int id)
    {
        var db = new NorthwindContex();
        return db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }

    public IList<ProductSearchModel> GetProductByName(string search)
    {
        throw new NotImplementedException();
    }

    public IList<Product> GetProducts()
    {
        var db = new NorthwindContex();
        return db.Products.Include(x => x.Category).ToList();
    }

    public bool UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }
}

