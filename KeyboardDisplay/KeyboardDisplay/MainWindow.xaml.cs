using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,0,0,0,1);
            dispatcherTimer.Start();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled)
            {
                CapsLock.prevstate = CapsLock.curstate;
                CapsLock.curstate = "on";
                if (CapsLock.prevstate != CapsLock.curstate)
                {
                    label1.Content = "On";
                    typeLabel.Content = "Caps Lock";
                    ShowChange();
                }
            }
            else if (!(Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.Toggled))
            {
                CapsLock.prevstate = CapsLock.curstate;
                CapsLock.curstate = "off";
                if (CapsLock.prevstate != CapsLock.curstate)
                {
                    label1.Content = "Off";
                    typeLabel.Content = "Caps Lock";
                    ShowChange();
                }
            }
            if (Keyboard.GetKeyStates(Key.NumLock) == KeyStates.Toggled)
            {
                NumLock.prevstate = NumLock.curstate;
                NumLock.curstate = "on";
                if (NumLock.prevstate != NumLock.curstate)
                {
                    label1.Content = "On";
                    typeLabel.Content = "Num Lock";
                    ShowChange();
                }
            }
            else if (!(Keyboard.GetKeyStates(Key.NumLock) == KeyStates.Toggled))
            {

                NumLock.prevstate = NumLock.curstate;
                NumLock.curstate = "off";
                if (NumLock.prevstate != NumLock.curstate)
                {
                    label1.Content = "Off";
                    typeLabel.Content = "Num Lock";
                    ShowChange();
                }
            
            }
            if (Keyboard.GetKeyStates(Key.Scroll) == KeyStates.Toggled)
            {
                ScrLock.prevstate = ScrLock.curstate;
                ScrLock.curstate = "on";
                if (ScrLock.prevstate != ScrLock.curstate)
                {
                    label1.Content = "On";
                    typeLabel.Content = "Scroll Lock";
                    ShowChange();
                }
            }
            else if (!(Keyboard.GetKeyStates(Key.Scroll) == KeyStates.Toggled))
            {

                ScrLock.prevstate = ScrLock.curstate;
                ScrLock.curstate = "off";
                if (ScrLock.prevstate != ScrLock.curstate)
                {
                    label1.Content = "Off";
                    typeLabel.Content = "Scroll Lock";
                    ShowChange();
                }

            }
        }
        private async void ShowChange()
        {
            Storyboard sb = this.FindResource("FadeIn") as Storyboard;
            Storyboard.SetTarget(sb, this);
            sb.Begin();
            await Task.Delay(5000);
            Storyboard sb2 = this.FindResource("FadeOut") as Storyboard;
            Storyboard.SetTarget(sb2, this);
            sb2.Begin();

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

