using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;


namespace api.Dtos.Stock
{
    // Dto Tanımlama
    // Mapper ile dönüşüm yapılacak modelin dışarıdan nasıl görüneceğini belirlemek için kullanılır.
    // Bu sayede modelin iç yapısı dışarıya kapalı tutulur ve dışarıdan erişim sağlanmaz.
    public class StockDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be more than 10 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Company Name cannot be more than 50 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(0, 10000000, ErrorMessage = "Purchase cannot be less than 0 or more than 10000000")]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Last Div cannot be more than 100")]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "Sector cannot be more than 40 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 50000000, ErrorMessage = "Market Cap cannot be less than 1 or more than 50000000")]
        public long MarketCap { get; set; }

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}