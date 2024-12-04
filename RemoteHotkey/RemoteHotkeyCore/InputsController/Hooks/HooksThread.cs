using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ht = System.ValueTuple<int, Hooks.HotkeyResponse>;
using HtEnter = System.ValueTuple<uint, Hooks.HotkeyModifiers, Hooks.HotkeyResponse, byte, int>;

namespace Hooks
{
    public class HooksThread
    {
        public event Action cleared;

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetMessage(out Win32Msg msg, IntPtr hWnd, uint filterMin, uint filterMax);
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private bool _running;
        public bool Running
        {
            get => _running;
            set
            {
                _running = value;
                if (Running)
                {
                    new System.Threading.Thread(StartMessageLoop).Start();
                }
            }
        }
        private const uint WM_HOTKEY = 0x0312U;
        private void StartMessageLoop()
        {
            hotkeys = new();
            while (Running)
            {
                cleared?.Invoke();

                while (queue.TryDequeue(out HtEnter action))
                {
                    if (Running == false)
                        return;

                    //Add Hotkey, fourth item (the byte) is 0
                    if (action.Item4 == 0)
                    {
                        if (RegisterHotKey(IntPtr.Zero, action.Item5, (uint)action.Item2, action.Item1)) hotkeys.Add((action.Item5, action.Item3));

                        if (Running == false)
                            return;
                    }
                    //Remove hotkey, fourth item is 1
                    else if (action.Item4 == 1)
                    {
                        if (Running == false)
                            return;

                        UnregisterHotKey(IntPtr.Zero, action.Item5);
                        //This just frees up some additional memory and doesnt leave it unnecessarily
                        foreach (Ht ht in hotkeys)
                        {
                            if (ht.Item1 == action.Item5)
                            {
                                hotkeys.Remove(ht);
                                //break from foreach otherwise ull get a collection modified error
                                break;
                            }
                        }

                        if (Running == false)
                            return;
                    }
                }
                //this will pretty much always return true which is why it shouldve been an if statement and i messed that up
                if (GetMessage(out Win32Msg msg, IntPtr.Zero, 0U, 0U))
                {
                    if (Running == false)
                        return;

                    //Message loop and if the windows message is the Hotkey message process it
                    switch (msg.Message)
                    {
                        case WM_HOTKEY:
                            //the msg's "wparam" hold an intptr to the id of the hotkey, and convert it to an int
                            int id = msg.WParam.ToInt32();
                            foreach (Ht ht in hotkeys)
                            {
                                //Check if the tuple holding the id and delegate matches so we can execute the correct delegate
                                if (ht.Item1 == id) ht.Item2(msg);
                            }
                            break;
                    }

                    if (Running == false)
                        return;
                }
            }
        }
        private List<Ht> hotkeys;
        private Random rand = new();
        private System.Collections.Concurrent.ConcurrentQueue<HtEnter> queue = new();
        //The return value of this method is the id of the hotkey which we need if we want to later unregister it
        public int AddHotkey(uint vk, HotkeyModifiers modifiers, HotkeyResponse response)
        {
            int id = rand.Next(0, 0xC000);
            queue.Enqueue((vk, modifiers, response, 0, id));
            return id;
        }
        public void RemoveHotkey(int id)
        {
            queue.Enqueue((0U, HotkeyModifiers.None, null, 1, id));
        }
    }
    public delegate void HotkeyResponse(Win32Msg msg);
    public enum HotkeyModifiers : uint
    {
        None = 0x0000U,
        Alt = 0x0001U,
        Ctrl = 0x0002U,
        Shift = 0x0004U,
        Win = 0x0008U,
        NoRepeat = 0x4000U
    }
    //Should work without it, but is good bc it specifies to put the fields sequentially which is important when working with the Win32 msg struct and passing between
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Msg
    {
        IntPtr hWnd;
        uint message;
        IntPtr wParam;
        IntPtr lParam;
        uint time;
        System.Drawing.Point pt;
        uint lPrivate;
        public uint Message { get => message; }
        public IntPtr WParam { get => wParam; }
        public IntPtr LParam { get => lParam; }
    }
}