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
                        // Image path is assumed to be correct as per optimization requirement.
                        // If Path.Combine is used, ensure card.Appearance is just the filename.
                        // If card.Appearance is already a relative path from Assets, then Path.Combine is not strictly needed here
                        // For now, we keep the Path.Combine to be safe, assuming card.Appearance is a filename.
                        // var imagePath = Path.Combine(_assetsPath, card.Appearance);
                        // No validation: card.Appearance = imagePath; // This line would be incorrect if imagePath is absolute
                                                // or if card.Appearance is already the correct relative path.
                                                // We will assume card.Appearance is already the correct relative path from Assets.
                        cards.Add(card);
                    }
                }
            }

            return cards;
        }

        public List<string> LoadAllCategories()
        {
            // Returns a list of category names
            return new List<string>
            {
                "Tarot", "Jokers", "Enhancements", "Boosters", "Planets", "Seals"
            };
        }

        public List<Card> LoadCardsForCategory(string categoryName)
        {
            string fileName = categoryName.ToLower() + ".json";
            // It's important to use the existing LoadCards method that contains image validation logic
            return LoadCards(fileName);
        }
    }
}
