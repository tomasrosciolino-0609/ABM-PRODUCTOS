using nuevoproyecto222.Entities;
using nuevoproyecto222.Models.DTOs.Reponses;
using nuevoproyecto222.Models.DTOs.Requests;
using System.Collections.Generic;

namespace nuevoproyecto222.Services
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        List<ProductForReadDto> SearchProductsByName(string name);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);
        ProductStatsDto GetStats();
    }
}
