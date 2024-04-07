using Microsoft.EntityFrameworkCore.Query.Internal;
using StocksApp.Dtos.Stock;
using StocksApp.Helpers;
using StocksApp.Models;

namespace StocksApp.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> GetByIdAsync(int Id);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<List<Stock?>> GetAllAsync(QueryObject query);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExist(int id);
    }
}
