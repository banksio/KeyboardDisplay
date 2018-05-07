using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KeyboardDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Storyboard fadeInStoryboard = new Storyboard();
        Storyboard fadeOutStoryboard = new Storyboard();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        bool visible = false;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer checkKeyLocks = new DispatcherTimer();
            checkKeyLocks.Tick += new EventHandler(checkKeyLocks_Tick);
            checkKeyLocks.Interval = new TimeSpan(0,0,0,0,1);
            checkKeyLocks.Start();

            DispatcherTimer fadeDelay = new DispatcherTimer();
            fadeDelay.Interval = new TimeSpan(0,0,5);
        }
        private void checkKeyLocks_Tick(object sender, EventArgs e)
        {
            checkKeyLocks();
            //Functions.changeDisplay(3);
        }

        private void checkKeyLocks()
        {
            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
            {
                if (Functions.changeStoredLock("CapsLock",true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.typeLabelText("CapsLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled))
            {
                if (Functions.changeStoredLock("CapsLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.typeLabelText("CapsLock");
                    ChangeDisplay();
                }
            }
            if (Keyboard.GetKeyStates(Key.NumLock) == KeyStates.Toggled)
            {
                //NumLock.prevstate = NumLock.curstate;
                //NumLock.curstate = "on";
                if (Functions.changeStoredLock("NumLock", true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.typeLabelText("NumLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.GetKeyStates(Key.NumLock) == KeyStates.Toggled))
            {

                //NumLock.prevstate = NumLock.curstate;
                //NumLock.curstate = "off";
                if (Functions.changeStoredLock("NumLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.typeLabelText("NumLock");
                    ChangeDisplay();
                }

            }
            if (Keyboard.GetKeyStates(Key.Scroll) == KeyStates.Toggled)
            {
                //ScrLock.prevstate = ScrLock.curstate;
                //ScrLock.curstate = "on";
                if (Functions.changeStoredLock("ScrLock", true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.typeLabelText("ScrLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.GetKeyStates(Key.Scroll) == KeyStates.Toggled))
            {

                //ScrLock.prevstate = ScrLock.curstate;
                //ScrLock.curstate = "off";
                if (Functions.changeStoredLock("ScrLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.typeLabelText("ScrLock");
                    ChangeDisplay();
                    
                }

            }
        }

        private void ChangeDisplay()
        {
            if (visible)
            {
                tokenSource.Cancel();
            }
            ShowChange(tokenSource.Token);
        }

        private async void ShowChange(CancellationToken token)
        {
            if (!visible) {
                visible = true;
                Storyboard sb = FindResource("FadeIn") as Storyboard;
                Storyboard.SetTarget(sb, this);
                sb.Begin();
            }
            try
            {
                await Task.Delay(5000, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            Storyboard sb2 = FindResource("FadeOut") as Storyboard;
            Storyboard.SetTarget(sb2, this);
            sb2.Begin();
            visible = false;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
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
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }

}

