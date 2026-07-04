using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.Http;

class BackgroundHandler
{
    private const int DoubleClickInterval = 500;
    private static DateTime _lastClickTime = DateTime.MinValue;
    private static HttpClient client = new HttpClient();
    private const int defaultDictionaryId = 1;

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int VK_CONTROL = 0x11;
    private const int VK_C = 0x43;

    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    [STAThread]
    static void Main()
    {
        _hookID = SetHook(_proc);

        Console.WriteLine("Программа запущена. Нажмите Ctrl+C дважды для отправки в API.");

        Application.Run();

        UnhookWindowsHookEx(_hookID);
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (vkCode == VK_C)
            {
                bool isCtrlPressed = (GetKeyState(VK_CONTROL) & 0x8000) != 0;

                if (isCtrlPressed)
                {
                    DateTime now = DateTime.Now;
                    double elapsed = (now - _lastClickTime).TotalMilliseconds;

                    if (elapsed < DoubleClickInterval)
                    {
                        _lastClickTime = DateTime.MinValue;
                        HandleAction();
                    }
                    else
                    {
                        _lastClickTime = now;
                    }
                }
            }
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    static async void HandleAction()
    {
        try
        {
            await Task.Delay(150);

            if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(text)) return;

                Console.WriteLine("Текст получен: " + text);

                string word = Uri.EscapeDataString(text);
                string translateUrl = $"https://english.werty.uk/api/Word/TranslateWord?Word={word}";
                var translate = await client.GetStringAsync(translateUrl);
                string url = $"https://english.werty.uk/api/Word/CreatePairWord?DitionaryId={defaultDictionaryId}&Word={word}&Translate={Uri.EscapeDataString(translate)}";

                Console.WriteLine("Отправка: " + url + " / " + translate);

                var response = await client.PostAsync(url, null);
                Console.WriteLine("Ответ сервера: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }

    #region WinAPI Imports
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern short GetKeyState(int keyCode);
    #endregion
}