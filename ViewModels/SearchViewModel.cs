using System.Collections.ObjectModel;
using System.Windows.Input;
using RaycastForWindows.Services;

namespace RaycastForWindows.ViewModels
{
    public class SearchViewModel
    {
        private readonly SearchService _searchService;

        public ObservableCollection<string> SearchResults { get; private set; }
        public string SearchQuery { get; set; }

        public ICommand SearchCommand { get; private set; }

        public SearchViewModel()
        {
            _searchService = new SearchService();
            SearchResults = new ObservableCollection<string>();
            SearchCommand = new RelayCommand(ExecuteSearch);
        }

        private void ExecuteSearch(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var results = _searchService.PerformSearch(SearchQuery);
                SearchResults.Clear();
                foreach (var result in results)
                {
                    SearchResults.Add(result);
                }
            }
        }
    }
}
