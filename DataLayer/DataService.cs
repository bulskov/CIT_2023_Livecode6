using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

    public (IList<Category> products, int count) GetCategories(int page, int pageSize)
    {
        var db = new NorthwindContex();
        var categories =
            db.Categories
             .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (categories, db.Categories.Count());
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
        var db = new NorthwindContex();
        return db.Products.Include(x => x.Category)
              .Where(x => x.Name.ToLower().Contains(search.ToLower()))
              .Select(x => new ProductSearchModel { ProductName = x.Name, CategoryName = x.Category.Name })
              .ToList();
    }

    public (IList<Product> products, int count) GetProducts(int page, int pageSize)
    {
        var db = new NorthwindContex();
        var products =  db.Products
            .Include(x => x.Category)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (products, db.Products.Count());
    }

    public bool UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }
}

