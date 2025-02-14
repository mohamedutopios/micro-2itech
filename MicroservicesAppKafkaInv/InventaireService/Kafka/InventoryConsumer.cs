using Confluent.Kafka;  
using InventaireService.DTO;  
using InventaireService.Models;  
using InventaireService.Services; 
using System.Text.Json;  

namespace InventaireService.Kafka
{
    public class InventoryConsumer
    {
        private readonly IServiceScopeFactory _scopeFactory; // Factory pour créer un scope DI
        private readonly string _topic = "order-placed"; // Nom du topic Kafka que ce consumer écoute
        private readonly string _groupId = "inventory-group"; // ID du groupe de consommateurs
        private readonly string _bootstrapServers = "localhost:9092"; // Adresse du serveur Kafka

        // Constructeur qui injecte la factory pour obtenir des instances des services
        public InventoryConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        // Démarrage de la consommation des messages Kafka
        public void StartConsuming()
        {
            // Configuration du consommateur Kafka
            var config = new ConsumerConfig
            {
                GroupId = _groupId, // Groupe de consommateurs Kafka
                BootstrapServers = _bootstrapServers, // Adresse du serveur Kafka
                AutoOffsetReset = AutoOffsetReset.Earliest // Lit tous les messages depuis le début si aucun offset n’est trouvé
            };

            // Création du consommateur Kafka
            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topic); // S’abonne au topic "order-placed"

            Console.WriteLine($"Listening for messages on topic: {_topic}");

            try
            {
                while (true) // Boucle infinie pour écouter les messages en continu
                {
                    var consumeResult = consumer.Consume(); // Récupère un message du topic

                    if (consumeResult != null && consumeResult.Message != null) // Vérifie si le message est valide
                    {
                        // Désérialise l'événement reçu en objet OrderPlacedEvent
                        var order = JsonSerializer.Deserialize<OrderPlacedEvent>(consumeResult.Message.Value);
                        Console.WriteLine($"Received order event for Product ID: {order.ProductId}, Quantity: {order.Quantity}");

                        // Crée un nouveau scope DI pour récupérer le service d'inventaire
                        using var scope = _scopeFactory.CreateScope();
                        var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();

                        // Récupère les informations de stock du produit commandé
                        var inventoryItem = inventoryService.GetStock(order.ProductId);

                        // Vérifie si le stock est suffisant pour valider la commande
                        if (inventoryItem != null && inventoryItem.Quantity >= order.Quantity)
                        {
                            // Met à jour la quantité en déduisant la commande
                            inventoryItem.Quantity -= order.Quantity;
                            inventoryService.UpdateStock(inventoryItem);
                            Console.WriteLine($"Stock updated for Product ID: {order.ProductId}. New Quantity: {inventoryItem.Quantity}");
                        }
                        else
                        {
                            Console.WriteLine($"Insufficient stock for Product ID: {order.ProductId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Kafka Consumer: {ex.Message}");
            }
        }
    }
}
