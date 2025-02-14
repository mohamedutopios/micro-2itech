using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderService.DTO
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public ProductDTO? ProductDetails { get; set; }
        public CustomerDTO? CustomerDetails { get; set; }
    }
}
