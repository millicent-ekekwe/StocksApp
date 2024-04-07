using System.ComponentModel.DataAnnotations;

namespace StocksApp.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
