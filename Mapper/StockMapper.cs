using StocksApp.Dtos.Stock;
using StocksApp.Models;

namespace StocksApp.Mapper
{
    public static class StockMapper
    {
        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase,
                MarketCap = stockDto.MarketCap,
               
            };
        }
        public static Stock ToStock(this Stock stockDto)
        {
            return new Stock
            {
                Id = stockDto.Id,
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                LastDiv = stockDto.LastDiv,
                Purchase = stockDto.Purchase,
                MarketCap = stockDto.MarketCap,
                Comments = stockDto.Comments
            };
        }
    }
}
