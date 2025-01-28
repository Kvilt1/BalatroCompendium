namespace RaycastForWindows.Models
{
    public class Card
    {
        public string Name { get; set; }
        public string Appearance { get; set; }
        public string Effect { get; set; }

        public Card(string name, string appearance, string effect)
        {
            Name = name;
            Appearance = appearance;
            Effect = effect;
        }
    }
}
