using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductsContext _productsContext ;

        public ProductsController(ProductsContext productsContext )
        {
            _productsContext = productsContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(){
            
            var products =await _productsContext.Products.Where(i => i.IsActive).Select(p =>GetProductDTO(p)).ToListAsync();
            return Ok(products);

            // return _products == null ? new List<Products>() : _products ;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id){
            
            if(id == null ){
                return NotFound();
            }

            var p = await _productsContext.Products.Where(i => i.ProductId == id).Select(p => GetProductDTO(p)).FirstOrDefaultAsync();

            if(p == null){
                return NotFound();
            }

            return Ok(p);

            // return _products?.FirstOrDefault(i => i.ProductId == id) ?? new Products();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products entity){

            _productsContext.Products.Add(entity);
            await _productsContext.SaveChangesAsync();
            return CreatedAtAction( nameof(GetProduct), new {id = entity.ProductId},entity);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int? id , Products entity){
            if(id != entity.ProductId){
                return BadRequest();
            }

            var product = await _productsContext.Products.FirstOrDefaultAsync(i => i.ProductId == entity.ProductId);
            
            if(product == null){
                return NotFound();
            }

            product.IsActive = entity.IsActive;  
            product.ProductName = entity.ProductName;  
            product.Price = entity.Price;  

            try{
                await _productsContext.SaveChangesAsync();
            }catch(Exception e ){
                Console.WriteLine(e);
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id){
            if(id == null){
                return NotFound();
            }

            var product = await _productsContext.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if(product == null){
                return NotFound();
            }

            try{
                await _productsContext.SaveChangesAsync();
            }catch(Exception e){
                Console.WriteLine(e);
                return NotFound();
            }
            return NoContent();
        }

        private static ProductDTO GetProductDTO(Products p){

            var entity = new ProductDTO();
            if(p != null){
                entity.Price = p.Price; 
                entity.ProductId = p.ProductId; 
                entity.ProductName = p.ProductName; 
            }

            return entity ;
        }

    }
}