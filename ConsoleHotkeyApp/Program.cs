using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

class BackgroundHandler
{
    private static Mutex? _mutex;

    private const int DoubleClickInterval = 500; // Интервал 500мс
    private static DateTime _lastCClickTime = DateTime.MinValue; // Время последнего нажатия Ctrl+C
    private static HttpClient client = new HttpClient();
    private const int defaultDictionaryId = 1;

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int VK_CONTROL = 0x11;
    private const int VK_C = 0x43;
    private const int VK_D = 0x44;

    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;

    [STAThread]
    static void Main()
    {
        bool createdNew;
        _mutex = new Mutex(true, @"Global\BackgroundHandler", out createdNew);

        if (!createdNew)
        {
            Console.WriteLine("Application is already running.");
            return;
        }

        _hookID = SetHook(_proc);

        Console.WriteLine("Программа запущена. Нажмите CTRL+C, затем D (в течение 500мс) для отправки в API.");

        Application.Run();

        UnhookWindowsHookEx(_hookID);
        GC.KeepAlive(_mutex);
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
            bool isCtrlPressed = (GetKeyState(VK_CONTROL) & 0x8000) != 0;

            if (isCtrlPressed)
            {
                if (vkCode == VK_C)
                {
                    _lastCClickTime = DateTime.Now;
                }
                else if (vkCode == VK_D)
                {
                    DateTime now = DateTime.Now;
                    double elapsed = (now - _lastCClickTime).TotalMilliseconds;

                    if (elapsed > 0 && elapsed < DoubleClickInterval)
                    {
                        _lastCClickTime = DateTime.MinValue; // Сбрасываем время
                        HandleAction();
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
            await Task.Delay(200);

            if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText().Trim();
                if (string.IsNullOrEmpty(text)) return;

                Console.WriteLine("Текст получен: " + text);

                string word = Uri.EscapeDataString(text);
                string translateUrl = $"https://english.werty.uk/api/Word/TranslateWord?Word={word}";

                var translateRaw = await client.GetStringAsync(translateUrl);
                string translate = translateRaw.Trim('"');

                string url = $"https://english.werty.uk/api/Word/CreatePairWord?DitionaryId={defaultDictionaryId}&Word={word}&Translate={Uri.EscapeDataString(translate)}";

                Console.WriteLine("Отправка: " + url);

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