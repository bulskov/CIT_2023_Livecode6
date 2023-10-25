using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataService
    {
        (IList<Category> products, int count) GetCategories(int page, int pageSize);
        Category? GetCategory(int id);
        (IList<Product> products, int count) GetProducts(int page, int pageSize);
        Product? GetProduct(int id);
        void CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int id);

        IList<ProductSearchModel> GetProductByName(string search);
    }
}