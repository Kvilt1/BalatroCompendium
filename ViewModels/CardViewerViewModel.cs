
namespace RaycastForWindows.ViewModels
{
    public class CardViewerViewModel
    {

    public string Name { get; set; }
    public string Description { get; set; }
    public void DisplayDetails()
    {
        // Logic to display card details
        Console.WriteLine($"{Name}: {Description}");
    }
    }
}


public void LoadCardData(string filePath)
{
    if (!File.Exists(filePath)) return;
    var jsonData = File.ReadAllText(filePath);
    var cards = JsonConvert.DeserializeObject<List<CardViewerViewModel>>(jsonData);
    foreach (var card in cards)
    {
        Console.WriteLine($"Loaded card: {card.Name} - {card.Description}");
    }
}
