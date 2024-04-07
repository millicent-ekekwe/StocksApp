using StocksApp.Interfaces;
using StocksApp.Models;
using System;
using Microsoft.EntityFrameworkCore;
using StocksApp.Data;
using StocksApp.Dtos.Stock;
using StocksApp.Helpers;

namespace StocksApp.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        }

       

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x =>x.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv=stockDto.LastDiv;
            existingStock.Industry= stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return (existingStock);
        }

        /* public async Task<List<Stock>> GetAllAsync(QueryObject query)
         {
             return await _context.Stocks.ToListAsync();
         }*/

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            //Filtering
            var stock = _context.Stocks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDescending ? stock.OrderByDescending(c => c.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }
            //Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;


            return await stock.Skip(skipNumber).Take(query.PageSize).Include(c => c.Comments).ToListAsync();
        }


        public async Task<Stock?> DeleteAsync(int id)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            {
                return null;
            }

            _context.Stocks.Remove(existingStock);
            await _context.SaveChangesAsync();
            return (existingStock);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }
    }
}
