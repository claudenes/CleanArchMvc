using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products Not Found");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProducts")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var products = await _productService.GetById(id);
            if (products == null)
            {
                return NotFound("products Not Found");
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
                return BadRequest("Invalid Data");
            await _productService.Add(productDTO);

            return new CreatedAtRouteResult("GetProducts", new { id = productDTO.Id }, productDTO);
        }
        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }
            if (productDTO == null)
            {
                return BadRequest();
            }
            await _productService.Update(productDTO);

            return Ok(productDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var products = await _productService.GetById(id);
            if (products == null)
            {
                return NotFound("Category not found");
            }
            await _productService.Remove(id);
            return Ok(products);
        }
    }
}
