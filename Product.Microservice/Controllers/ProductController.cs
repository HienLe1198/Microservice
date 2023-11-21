using Catalog.Microservice.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Microservice.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Product.Microservice.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Item>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Item>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Item>> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Item>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Item>>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);
            return Ok(products);
        }

        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Item>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Item>>> GetProductByName(string name)
        {
            var items = await _productRepository.GetProductByName(name);
            if (items == null)
            {
                _logger.LogError($"Products with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Item>> CreateProduct([FromBody] Item product)
        {
            await _productRepository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Item product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            List<int> ls = new List<int>();
            ls.Add(1);
            return Ok(await _productRepository.DeleteProduct(id));
        }
    }
}
