/****************************************************************************
 * Copyright 2012 Nikolay Manolov
 * 
 * This file is part of NinjaSnipper.

    NinjaSnipper is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    NinjaSnipper is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with NinjaSnipper.  If not, see <http://www.gnu.org/licenses/>.
 * ************************************************************************/
// Due Credit:
// http://sdaaubckp.svn.sourceforge.net/viewvc/sdaaubckp/xp-take-screenshot/?view=tar
//http://stackoverflow.com/questions/158151/how-can-i-save-a-screenshot-directly-to-a-file-in-windows
//Stephen Toub : Low-Level Keyboard Hook in C# - 
//http://blogs.msdn.com/toub/archive/2006/05/03/589423.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Globalization; //CultureInfo
// from keyboard hook: 
using System.Diagnostics;
using System.Windows.Forms; // for Application and Keys

namespace NinjaSnipper
{
    class PrintScreenHook
    {
        // from keyboard hook: 
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static bool ctrlPressed = false;
        private static bool spacePressed = false;
        private static bool cPressed = false;
        public static LowLevelKeyboardProc _proc = HookCallback;
        public static IntPtr _hookID = IntPtr.Zero;

        /* KEYBOARD HOOK RELATED */
        public static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN){
                int vkCode = Marshal.ReadInt32(lParam);
                //Console.WriteLine((Keys)vkCode);
                // well no need to output usual chars here - uncomment below line to debug
                //Console.WriteLine("It is: " + (Keys)vkCode + " - " + vkCode);

                if (vkCode == 0X2C){ //Print Screen
                    if (Properties.Settings.Default.CatchPrintScreen && Properties.Settings.Default.SaveToFile)
                    {
                        // The printscreen button has been pressed, however the screen has not yet been captured
                        // so we schedule the saving of the clipboard and forward the event.
                        ScreenShot.scheduleClipboardRead(); 
                    }
                }

                if (vkCode == 0XA2) { PrintScreenHook.ctrlPressed = true; }
                if (vkCode == 0X20) { PrintScreenHook.spacePressed = true;  }
                if (vkCode == 0X43) { PrintScreenHook.cPressed = true;  }
                if (PrintScreenHook.ctrlPressed && PrintScreenHook.spacePressed && PrintScreenHook.cPressed){
                    SnipForm.doSnip();
                }
            }
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP){
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == 0XA2) PrintScreenHook.ctrlPressed = false;
                if (vkCode == 0X20) PrintScreenHook.spacePressed = false;
                if (vkCode == 0X43) PrintScreenHook.cPressed = false;
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /* END KEYBOARD HOOK RELATED */

        //protected static IntPtr m_HBitmap;
    }

    public class WIN32_API
    {
        public struct SIZE
        {
            public int cx;
            public int cy;
        }
        public const int SRCCOPY = 13369376;
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
    }
}
