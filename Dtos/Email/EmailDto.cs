using System.ComponentModel.DataAnnotations;

namespace StocksApp.Dtos.Email
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string OTP { get; set; }
        //public string Body { get; set; } = string.Empty;
    }
}
