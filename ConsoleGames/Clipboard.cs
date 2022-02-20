using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames
{
    public static class Clipboard
    {
        [DllImport("user32.dll")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool SetClipboardData(uint uFormat, IntPtr data);

        [STAThread]
        public static void Copy(string text)
        {
            OpenClipboard(IntPtr.Zero);
            var ptr = Marshal.StringToHGlobalUni(text);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
        }
    }
}
