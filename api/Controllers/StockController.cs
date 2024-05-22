using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStock()
        {
            var stock = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null)
            {
                return NotFound(new { message = "Stock not found" });

            }
            else
            {
                return Ok(stock.ToStockDto());
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockCsreateDto)
        {
            var stock = stockCsreateDto.ToStockFromCreateDto();
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockDto());
        }



    }
}