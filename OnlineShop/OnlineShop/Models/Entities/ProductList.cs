using inceputproiectMds.Models.Entities;
using OnlineShop.Models.DTOs;

namespace OnlineShop.Models.Entities
{
    public class ProductList
    {
        public List<ProductSendDTO> Products { get; set; }
        public int Total { get; set; }

    }
}
