using System.Windows.Input;
using System.Windows;
using RaycastForWindows.Services;

namespace RaycastForWindows.Views
{
    public partial class MainWindow : Window
{

        private void OnMouseEnterCard(object sender, MouseEventArgs e)
        {

    private void OnMouseEnterCard(object sender, MouseEventArgs e)
    {
        // Show a tooltip with card details
        var card = (sender as FrameworkElement)?.DataContext as CardViewerViewModel;
        if (card != null)
        {
            TooltipService.ShowTooltip(sender as FrameworkElement, $"{card.Name}: {card.Description}" };
            (sender as FrameworkElement).ToolTip = toolTip;
        }
    }
        }

public partial class MainWindow : Window
{
    private void OnMouseEnterCard(object sender, MouseEventArgs e)
    {

    private void OnMouseEnterCard(object sender, MouseEventArgs e)
    {
        // Show a tooltip with card details
        var card = (sender as FrameworkElement)?.DataContext as CardViewerViewModel;
        if (card != null)
        {
            TooltipService.ShowTooltip(sender as FrameworkElement, $"{card.Name}: {card.Description}" };
            (sender as FrameworkElement).ToolTip = toolTip;
        }
    }
    }

public partial class MainWindow : Window
{
    private void OnMouseLeaveCard(object sender, MouseEventArgs e)
    {

    private void OnMouseLeaveCard(object sender, MouseEventArgs e)
    {
        // Remove the tooltip
        TooltipService.HideTooltip(sender as FrameworkElement);
    }
    }

public partial class MainWindow : Window
{
        private void OnMouseLeaveCard(object sender, MouseEventArgs e)
        {

    private void OnMouseLeaveCard(object sender, MouseEventArgs e)
    {
        // Remove the tooltip
        TooltipService.HideTooltip(sender as FrameworkElement);
    }
        }
        
}
    {
public partial class MainWindow : Window
{

        public MainWindow()

    public MainWindow()
    {
        InitializeComponent();
        var cardViewer = new CardViewerViewModel();
        cardViewer.LoadCardData("src/jokers.json");
        cardViewer.LoadCardData("src/jokers.json"); // Ensure the file path is correct
                {
            InitializeComponent();
            _shortcutService = new GlobalShortcutService(this);
        }

public partial class MainWindow : Window
{
        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}
}
}
}
}
