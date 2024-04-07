using StocksApp.Models;

namespace StocksApp.Interfaces
{
    public interface ICommentRepository
    {
       Task<Comment> CreateAsync(Comment commentModel);
       Task<Comment> GetByIdAsync(int id);
    }
}
