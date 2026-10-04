using System.Collections.Generic;
using nuevoproyecto222.Entities;

namespace nuevoproyecto222.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product? GetProductById(int id);
        List<Product> SearchProductsByName(string name);
        bool ExistsByName(string name); 
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}

