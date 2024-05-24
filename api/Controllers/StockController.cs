using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepository.GetAllAsync();
            var stockDto = stocks.Select(s => s.MapToStockDto());
            return Ok(stocks);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetStockById(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepository.GetByIdAsync(id);
            if (stockModel == null)
            {
                return NotFound(new { message = "Stock not found" });

            }
            return Ok(stockModel.MapToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockReqDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockReqDto.MapToStockFromCreateDto();
            await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.MapToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockUpdateDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepository.UpdateAsync(id, stockUpdateDto);

            if (stockModel == null)
            {
                return NotFound(new { message = "Stock not found" });
            }

            return Ok(stockModel.MapToStockDto());
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.DeleteAsync(id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found" });
            }

            return NoContent();
        }

    }
}