using Microsoft.AspNetCore.Mvc;
using nuevoproyecto222.Models.DTOs.Reponses;
using nuevoproyecto222.Models.DTOs.Requests;
using nuevoproyecto222.Services;
using System.Collections.Generic;

namespace nuevoproyecto222.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductForReadDto>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductForReadDto> GetProductById(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductForReadDto> CreateProduct([FromBody] ProductForCreateDto dto)
        {
            try
            {
                var createdProduct = _productService.CreateProduct(dto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductForUpdateDto dto)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.UpdateProduct(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);

            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<List<ProductForReadDto>> SearchProducts([FromQuery] string name)
        {
            var products = _productService.SearchProductsByName(name);
            return Ok(products);
        }

        [HttpGet("stats")]
        public ActionResult<ProductStatsDto> GetProductStats()
        {
            var stats = _productService.GetStats();
            return Ok(stats);
        }
    }
}
