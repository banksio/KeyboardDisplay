using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace KeyboardDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public bool startUp = true;

        private NotifyIcon ni;

        //Disable window focus
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        //for fade-in/out
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        //for detecting keypresses
        private KeyboardHook _hook;

        public NotifyIcon Ni { get => ni; set => ni = value; }

        public MainWindow()
        {
            InitializeComponent();

            CreateNotifyIcon();
            ni.BalloonTipClicked += new EventHandler(GetNewUpdate);

            //DispatcherTimer fadeDelay = new DispatcherTimer();
            //fadeDelay.Interval = new TimeSpan(0,0,5);

            _hook = new KeyboardHook();
            _hook.KeyUp += new KeyboardHook.HookEventHandler(OnHookKeyUp);

            //check for updates last, it is least important
            GetUpdateInfo();

        }
               
        public void CreateNotifyIcon()
        {
            Ni = new System.Windows.Forms.NotifyIcon();
            Ni.Icon = System.Drawing.Icon.ExtractAssociatedIcon(
                         System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            Ni.Visible = true;
        }

        void OnHookKeyUp(object sender, HookEventArgs e)
        {
            checkKeyLocks();
        }

        private void checkKeyLocks()
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock))
            {
                if (Functions.ChangeStoredLock("CapsLock",true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.TypeLabelText("CapsLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.CapsLock)))
            {
                if (Functions.ChangeStoredLock("CapsLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.TypeLabelText("CapsLock");
                    ChangeDisplay();
                }
            }
            if (Keyboard.IsKeyToggled(Key.NumLock))
            {
                //NumLock.prevstate = NumLock.curstate;
                //NumLock.curstate = "on";
                if (Functions.ChangeStoredLock("NumLock", true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.TypeLabelText("NumLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.NumLock)))
            {

                //NumLock.prevstate = NumLock.curstate;
                //NumLock.curstate = "off";
                if (Functions.ChangeStoredLock("NumLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.TypeLabelText("NumLock");
                    ChangeDisplay();
                }

            }
            if (Keyboard.IsKeyToggled(Key.Scroll))
            {
                //ScrLock.prevstate = ScrLock.curstate;
                //ScrLock.curstate = "on";
                if (Functions.ChangeStoredLock("ScrLock", true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.TypeLabelText("ScrLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.Scroll)))
            {

                //ScrLock.prevstate = ScrLock.curstate;
                //ScrLock.curstate = "off";
                if (Functions.ChangeStoredLock("ScrLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.TypeLabelText("ScrLock");
                    ChangeDisplay();
                    
                }

            }
        }

        private void ChangeDisplay()
        {
            if (startUp) { return; }
            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = new CancellationTokenSource();
            ShowChange(tokenSource.Token);
        }

        private async void ShowChange(CancellationToken token)
        {
            Storyboard sb = FindResource("FadeIn") as Storyboard;
            Storyboard.SetTarget(sb, this);
            sb.Begin();
            try
            {
                await Task.Delay(5000, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            finally
            {
                //tokenSource.Dispose();
            }
            if (Properties.Settings.Default.alwaysOn == false)
            {
                Storyboard sb2 = FindResource("FadeOut") as Storyboard;
                Storyboard.SetTarget(sb2, this);
                sb2.Begin();
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);

            // Set the window style to noactivate.
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SetWindowLong(helper.Handle, GWL_EXSTYLE,
            GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        public static class WindowsServices
        {
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int GWL_EXSTYLE = (-20);

            [DllImport("user32.dll")]
            static extern int GetWindowLong(IntPtr hwnd, int index);

            [DllImport("user32.dll")]
            static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

            public static void SetWindowExTransparent(IntPtr hwnd)
            {
                var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkKeyLocks();
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            startUp = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 settings = new Window1();
            settings.Show();
        }

        private void GetUpdateInfo()
        {
            // Set URL.
            var url = "https://raw.githubusercontent.com/banksio/KeyboardDisplay/master/versions.txt";

            var total = 0;
            // GetURLContents returns the contents of url as a byte array.
            byte[] urlContents = GetURLContents(url);

            ProcessURLResponse(url, urlContents);


            // Update the total.
            total += urlContents.Length;

            // Display the total count for all of the web addresses.
            //resultsTextBox.Text += $"\r\n\r\nTotal bytes returned:  {total}\r\n";
        }

        private byte[] GetURLContents(string url)
        {
            // The downloaded resource ends up in the variable named content.
            var content = new MemoryStream();

            // Initialize an HttpWebRequest for the current URL.
            var webReq = (HttpWebRequest)WebRequest.Create(url);

            // Send the request to the Internet resource and wait for
            // the response.
            // Note: you can't use HttpWebRequest.GetResponse in a Windows Store app.
            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.
                    responseStream.CopyTo(content);
                }
            }

            // Return the result as a byte array.
            return content.ToArray();
        }

        private void ProcessURLResponse(string url, byte[] content)
        {
            // Display the length of each website. The string format
            // is designed to be used with a monospaced font, such as
            // Lucida Console or Global Monospace.
            var bytes = content.Length;
            var contentString = Regex.Replace(Encoding.UTF8.GetString(content), @"\t|\n|\r", "");


            if (int.Parse(contentString.Replace(@".", "")) > int.Parse(Properties.Resources.version.Replace(@".", "")))
            {
                //show update notification
                Ni.ShowBalloonTip(1, "Keyboard Display Update", "Version "+contentString+" is available. Tap or click here to install it.", ToolTipIcon.Info);
            }
        }

        private void GetNewUpdate(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/banksio/KeyboardDisplay/releases/latest");
        }

    }


}

