using System;
using System.Runtime.InteropServices;

namespace TheGame
{
	public static class WinApi
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cX, int cY, uint uFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetStdHandle(int stdHandle);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetConsoleTitle(string strMessage);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, [In, Out] CONSOLE_FONT_INFOEX consoleCurrentFontEx);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool GetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, [In, Out] CONSOLE_FONT_INFOEX consoleCurrentFontEx);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private class CONSOLE_FONT_INFOEX
		{
			private int cbSize;
			public CONSOLE_FONT_INFOEX()
			{
				cbSize = Marshal.SizeOf(typeof(CONSOLE_FONT_INFOEX));
			}
			public int FontIndex;
			public COORD FontSize;
			public int FontFamily;
			public int FontWeight;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string FaceName;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct COORD
		{
			private short X;
			private short Y;

			public COORD(short posX, short posY)
			{
				X = posX;
				Y = posY;
			}
		};

		// Method's
		public static void SetConsoleTitleName(String title)
		{
			SetConsoleTitle(title);
		}

		public static void SetConsoleWindowPosition(int x, int y)
		{
			IntPtr HWND_TOPMOST = new IntPtr(-1);
			UInt32 SWP_NOSIZE = 0x0001;
			UInt32 SWP_SHOWWINDOW = 0x0040;
			IntPtr hWnd = GetConsoleWindow();
			SetWindowPos(hWnd, HWND_TOPMOST, x, y, 0, 0, SWP_NOSIZE | SWP_SHOWWINDOW);
		}

		public static void SetConsoleFont(String fontName)
		{
			CONSOLE_FONT_INFOEX info = new CONSOLE_FONT_INFOEX();
			info.FaceName = fontName;
			SetCurrentConsoleFontEx(GetStdHandle(-11), false, info);
		}

		public static void SetConsoleFontSize(short x, short y)
		{
			int STD_OUTPUT_HANDLE = -11;
			COORD size = new COORD( x, y );
			CONSOLE_FONT_INFOEX info = new CONSOLE_FONT_INFOEX();
			GetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, info);
			info.FontSize = size;
			SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), false, info);
		}

		public static void Exit()
		{
			IntPtr hWnd = GetConsoleWindow();
			UInt32 WM_CLOSE = 0x0010;
			PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
		}
	}
}
