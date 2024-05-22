using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Stock
{
    // Dto Tanımlama
    // Mapper ile dönüşüm yapılacak modelin dışarıdan nasıl görüneceğini belirlemek için kullanılır.
    // Bu sayede modelin iç yapısı dışarıya kapalı tutulur ve dışarıdan erişim sağlanmaz.
    public class CreateStockRequestDto
    {

        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}