﻿using StocksApp.Dtos.Comment;
using StocksApp.Dtos.Stock;
using StocksApp.Models;

namespace StocksApp.Mapper
{
    public static class CommentMapper
    {
        public static Comment ToCommentFromCreate(this CreateCommentDto commentModel, int stockId)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = stockId
            };

        }
        public static CommentDto ToComment(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };

        }
    }
}
