using System.ComponentModel.DataAnnotations;

namespace StocksApp.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be greater than 5 characters")]
        [MaxLength(280, ErrorMessage = "Title must not be more than 280 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be greater than 5 characters")]
        [MaxLength(280, ErrorMessage = "Content must not be more than 280 characters")]
        public string Content { get; set; } = string.Empty;

       
        
    }
}
