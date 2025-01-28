using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RaycastForWindows.Models;

namespace RaycastForWindows.Services
{
    public class CardService
    {
        private readonly string _basePath;
        private readonly string _assetsPath;

        public CardService(string basePath)
        {
            _basePath = basePath;
            _assetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
        }

        public List<Card> LoadCards(string categoryFile)
        {
            var cards = new List<Card>();
            var filePath = Path.Combine(_basePath, categoryFile);

            if (File.Exists(filePath))
            {
                var jsonContent = File.ReadAllText(filePath);
                var parsedCards = JsonSerializer.Deserialize<List<Card>>(jsonContent);
                if (parsedCards != null)
                {
                    foreach (var card in parsedCards)
                    {
                        // Validate image existence
                        var imagePath = Path.Combine(_assetsPath, card.Appearance);
                        if (!File.Exists(imagePath))
                        {
                            card.Appearance = "extension_icon.png"; // Default placeholder image
                        }
                        cards.Add(card);
                    }
                }
            }

            return cards;
        }

        public Dictionary<string, List<Card>> LoadAllCategories()
        {
            var categories = new Dictionary<string, List<Card>>
            {
                { "Tarot", LoadCards("tarot.json") },
                { "Jokers", LoadCards("jokers.json") },
                { "Enhancements", LoadCards("enhancements.json") },
                { "Boosters", LoadCards("boosters.json") },
                { "Planets", LoadCards("planets.json") },
                { "Seals", LoadCards("seals.json") }
            };

            return categories;
        }
    }
}
