using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_Product_Management_System.BLL.Product;
using Simple_Product_Management_System.Dto;
using System;

namespace Simple_Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class ProdectController : ControllerBase
    {
        private readonly IProdect _productService;
        private readonly IValidator<ProductDTO> validator;

        public ProdectController(IProdect productService, IValidator<ProductDTO> validator)
        {
            _productService = productService;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productExists = await _productService.GetByID(id);
            if (productExists==null) return NotFound();

            return Ok(productExists);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDTO productDTO)
        {
            var validationResult = await validator.ValidateAsync(productDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);



            var result = await _productService.Add(productDTO);
            if (!result) return BadRequest("Unable to add product.");

            return CreatedAtAction(nameof(GetById), new { id = productDTO.Id }, productDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            var validationResult = await validator.ValidateAsync(productDTO);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            productDTO.Id = id;
            var result = await _productService.Update(productDTO);
            if (!result) return NotFound();
            var productExists = await _productService.GetByID(id);
            if (productExists == null) return NotFound();

            return Ok(productExists);
         

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (!result) return NotFound();

            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var products = await _productService.Search(query);
            return Ok(products);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] string columnName)
        {
            var products = await _productService.Sort(columnName);
            return Ok(products);
        }
    }
}
