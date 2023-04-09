using inceputproiectMds.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inceputproiectMds.Models.Entities
{
    public class Cart
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
