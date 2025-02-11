using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderService.Models
{
    public class Order
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id { get; set; }

        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    }
}
