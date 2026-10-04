using nuevoproyecto222.Entities;
using nuevoproyecto222.Models.DTOs.Reponses;
using nuevoproyecto222.Models.DTOs.Requests;
using nuevoproyecto222.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace nuevoproyecto222.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<ProductForReadDto> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();

            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public ProductForReadDto? GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null) return null;

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            if (_productRepository.ExistsByName(dto.Name))
            {
                throw new InvalidOperationException("Ya existe un producto con ese nombre.");
            }

            var newId = _productRepository.GetAllProducts().Max(p => p.Id) + 1;

            var product = new Product
            {
                Id = newId,
                Name = dto.Name,
                Price = dto.Price
            };

            _productRepository.AddProduct(product);

            return new ProductForReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct != null)
            {
                existingProduct.Name = dto.Name;
                existingProduct.Price = dto.Price;

                _productRepository.UpdateProduct(existingProduct);
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                _productRepository.DeleteProduct(product);
            }
        }

        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            var products = _productRepository.SearchProductsByName(name);

            return products.Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public ProductStatsDto GetStats()
        {
            var products = _productRepository.GetAllProducts();

            if (products == null || products.Count == 0)
            {
                return new ProductStatsDto
                {
                    Total = 0,
                    AveragePrice = 0,
                    MostExpensiveName = "N/A"
                };
            }

            var total = products.Count;
            var averagePrice = products.Average(p => p.Price);

            var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
            var mostExpensiveName = mostExpensive?.Name ?? "N/A";

            return new ProductStatsDto
            {
                Total = total,
                AveragePrice = averagePrice,
                MostExpensiveName = mostExpensiveName
            };
        }
    }
}