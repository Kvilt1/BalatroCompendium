using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace RaycastForWindows.Services
{
    public class GlobalShortcutService
    {
        private const int HOTKEY_ID = 9000; // Unique ID for the hotkey
        private const uint MOD_CONTROL = 0x0002; // CTRL key modifier
        private const uint MOD_ALT = 0x0001; // ALT key modifier
        private const uint VK_SPACE = 0x20;  // SPACE key

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly IntPtr _windowHandle;

        public GlobalShortcutService(Window window)
        {
            _windowHandle = new WindowInteropHelper(window).Handle;
            RegisterGlobalHotkey();
            HookMessages();
        }

        private void RegisterGlobalHotkey()
        {
            if (!RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL | MOD_ALT, VK_SPACE))
            {
                MessageBox.Show("Failed to register global shortcut.");
            }
        }

        private void HookMessages()
        {
            var source = HwndSource.FromHwnd(_windowHandle);
            source.AddHook(HwndHook);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            if (msg == WM_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
            {
                OnHotkeyPressed();
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void OnHotkeyPressed()
        {
            // Logic to trigger when the global shortcut is pressed
            MessageBox.Show("Global Shortcut Triggered!");
        }

        public void Dispose()
        {
            UnregisterHotKey(_windowHandle, HOTKEY_ID);
        }
    }
}
