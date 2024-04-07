using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StocksApp.Dtos.Stock;
using StocksApp.Helpers;
using StocksApp.Interfaces;
using StocksApp.Mapper;

namespace StocksApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null) return NotFound();

            return Ok(stock.ToStock());
        }

        [HttpPost("create")]
        /*[Authorize]*/
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = stockDto.ToStockFromCreateDto(); //maps stockdto to the stock model

            await _stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStock());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> update([FromRoute] int id, UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Check if the stock exist before updating
            var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStock());
        }
        /* [HttpGet]
         *//* public async Task<IActionResult> GetAll()
          {
              if (!ModelState.IsValid) return BadRequest(ModelState);
              var stocks = await _stockRepository.GetAllAsync();

              return Ok(stocks);
          }*/
        [HttpGet ("AllAndSearch")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepository.GetAllAsync(query);
           /* var stockDto = stocks.Select(s => s.ToStock());*/

            return Ok(stocks);
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepository.DeleteAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
