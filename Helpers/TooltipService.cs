
using System.Windows;
using System.Windows.Controls;

public static class TooltipService
{
    public static void ShowTooltip(FrameworkElement element, string content)
    {
        if (element == null) return;
        ToolTip toolTip = new ToolTip { Content = content };
        element.ToolTip = toolTip;
    }

    public static void HideTooltip(FrameworkElement element)
    {
        if (element == null) return;
        element.ToolTip = null;
    }
}
