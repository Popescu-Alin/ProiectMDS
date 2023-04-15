using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class ProductOrder: BaseEntity
    {
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
