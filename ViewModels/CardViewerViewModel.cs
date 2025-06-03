using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using RaycastForWindows.Models;
using RaycastForWindows.Services;

namespace RaycastForWindows.ViewModels
{
    public class CardViewerViewModel : INotifyPropertyChanged
    {
        private readonly CardService _cardService;
        private List<string> _cardCategories;
        private string _selectedCategory;
        private ObservableCollection<Card> _displayedCards;
        private Dictionary<string, List<Card>> _loadedCardsCache = new Dictionary<string, List<Card>>();

        public CardViewerViewModel()
        {
            // Assuming CardService expects the base path to "Data" directory
            // Adjust the path as per actual project structure if necessary
            string basePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            _cardService = new CardService(basePath);
            LoadCategoryNames();
        }

        public List<string> CardCategories
        {
            get => _cardCategories;
            set
            {
                _cardCategories = value;
                OnPropertyChanged(nameof(CardCategories));
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    LoadCardsForSelectedCategory();
                }
            }
        }

        public ObservableCollection<Card> DisplayedCards
        {
            get => _displayedCards;
            set
            {
                _displayedCards = value;
                OnPropertyChanged(nameof(DisplayedCards));
            }
        }

        private void LoadCategoryNames()
        {
            CardCategories = _cardService.LoadAllCategories();
            // Optionally, select the first category by default
            if (CardCategories != null && CardCategories.Any())
            {
                SelectedCategory = CardCategories.First();
            }
        }

        private void LoadCardsForSelectedCategory()
        {
            if (string.IsNullOrEmpty(SelectedCategory))
            {
                DisplayedCards = new ObservableCollection<Card>();
                return;
            }

            if (_loadedCardsCache.TryGetValue(SelectedCategory, out var cachedCards))
            {
                DisplayedCards = new ObservableCollection<Card>(cachedCards);
            }
            else
            {
                var cards = _cardService.LoadCardsForCategory(SelectedCategory);
                _loadedCardsCache[SelectedCategory] = cards; // Cache the loaded cards
                DisplayedCards = new ObservableCollection<Card>(cards);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
