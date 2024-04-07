using Microsoft.EntityFrameworkCore;
using StocksApp.Data;
using StocksApp.Interfaces;
using StocksApp.Models;

namespace StocksApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(s=>s.Stock).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
