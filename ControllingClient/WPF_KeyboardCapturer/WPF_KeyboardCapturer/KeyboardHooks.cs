﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace WPF_KeyboardCapturer
{
    public class KeyboardHooks
    {
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, int wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hook);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, int wParam, IntPtr lParam);
        private LowLevelKeyboardProc keyboardProcess;

        public static IntPtr ptrHook;

        public event System.Windows.Forms.KeyEventHandler KeyUp;
        public event System.Windows.Forms.KeyEventHandler KeyDown;

        public KeyboardHooks()
        {
            Hook();
        }

        ~KeyboardHooks()
        {
            Unhook();
        }

        public void Hook()
        {
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            keyboardProcess = new LowLevelKeyboardProc(CaptureKey);
            ptrHook = SetWindowsHookEx(13, keyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }
        public void Unhook()
        {
            UnhookWindowsHookEx(ptrHook);
        }

        private IntPtr CaptureKey(int nCode, int wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT keyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));
                System.Windows.Forms.KeyEventArgs eventArgs = new System.Windows.Forms.KeyEventArgs(keyInfo.key);
                if ((wp == WM_KEYDOWN || wp == WM_SYSKEYDOWN) && KeyDown != null)
                {
                    KeyDown(this, eventArgs);
                }
                else if ((wp == WM_KEYUP || wp == WM_SYSKEYUP) && (KeyUp != null))
                {
                    KeyUp(this, eventArgs);
                }
                if (eventArgs.Handled)
                    return (IntPtr)1;
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }
    }
}