using Microsoft.EntityFrameworkCore;
using nuevoproyecto222.Data;
using nuevoproyecto222.Entities;

namespace nuevoproyecto222.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly nuevoproyecto222Context _context;

        public ProductRepository(nuevoproyecto222Context context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products
                .OrderBy(p => p.Name)
                .ToList();
        }

        public Product? GetProductById(int id)
        {
            return _context.Products
                .FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                _context.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
                _context.SaveChanges();
            }
        }

        public List<Product> SearchProductsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return GetAllProducts();
            }

            var lowerName = name.ToLower();
            return _context.Products
                .Where(p => p.Name.ToLower().Contains(lowerName))
                .ToList();
        }

        public bool ExistsByName(string name)
        {
            var lowerName = name.ToLower();
            return _context.Products
                .Any(p => p.Name.ToLower() == lowerName);
        }
    }
}