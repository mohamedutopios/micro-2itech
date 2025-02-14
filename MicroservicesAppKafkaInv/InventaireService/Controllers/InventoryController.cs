using Confluent.Kafka;
using InventaireService.Data;
using InventaireService.Models;
using InventaireService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InventaireService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;
         
        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetInventory() => Ok(_service.GetInventory());

        [HttpGet("{productId}")]
        public IActionResult GetStock(int productId)
        {
            var item = _service.GetStock(productId);
            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public IActionResult AddStock([FromBody] InventoryItem item)
        {
            _service.AddStock(item);
            return CreatedAtAction(nameof(GetStock), new { productId = item.ProductId }, item);
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateStock(int productId, [FromBody] InventoryItem updatedItem)
        {
            _service.UpdateStock(updatedItem);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteStock(int productId)
        {
            _service.DeleteStock(productId);
            return NoContent();
        }
    }
}