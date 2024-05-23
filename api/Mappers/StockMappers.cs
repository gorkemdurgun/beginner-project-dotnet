using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{

    // Mapper Tanımlama
    // Stock modeli ile StockDto arasında dönüşüm yapmak için kullanılır.
    // Bu işlemin yapılma amacı, veritabanı işlemleri yapılırken modelin dışarıya nasıl gönderileceğini belirlemektir.
    // Bu sayede modelin iç yapısı dışarıya kapalı tutulur ve dışarıdan erişim sağlanmaz.
    public static class StockMappers
    {
        public static StockDto MapToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.MapToCommentDto()).ToList()
            };
        }

        public static Stock MapToStockFromCreateDto(this CreateStockRequestDto stockCreateDto)
        {
            return new Stock
            {
                Symbol = stockCreateDto.Symbol,
                CompanyName = stockCreateDto.CompanyName,
                Purchase = stockCreateDto.Purchase,
                LastDiv = stockCreateDto.LastDiv,
                Industry = stockCreateDto.Industry,
                MarketCap = stockCreateDto.MarketCap
            };
        }

    }
}