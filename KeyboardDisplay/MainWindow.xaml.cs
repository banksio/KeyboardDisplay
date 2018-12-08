﻿using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetAsyncKeyState(int keyCode);

        Storyboard fadeInStoryboard = new Storyboard();
        Storyboard fadeOutStoryboard = new Storyboard();
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        private KeyboardHook _hook;
        
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer fadeDelay = new DispatcherTimer();
            fadeDelay.Interval = new TimeSpan(0,0,5);

            _hook = new KeyboardHook();
            _hook.KeyUp += new KeyboardHook.HookEventHandler(OnHookKeyUp);

        }

        void OnHookKeyUp(object sender, HookEventArgs e)
        {
            checkKeyLocks();
        }

        private void checkKeyLocks()
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock))
            {
                if (Functions.changeStoredLock("CapsLock",true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.typeLabelText("CapsLock");
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.CapsLock)))
            {
                if (Functions.changeStoredLock("CapsLock", false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.typeLabelText("CapsLock");
                    ChangeDisplay();
                }
            }
            if (Keyboard.IsKeyToggled(Key.NumLock))
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
            else if (!(Keyboard.IsKeyToggled(Key.NumLock)))
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
            if (Keyboard.IsKeyToggled(Key.Scroll))
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
            else if (!(Keyboard.IsKeyToggled(Key.Scroll)))
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
            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = new CancellationTokenSource();
            ShowChange(tokenSource.Token);
        }

        private async void ShowChange(CancellationToken token)
        {
            //this.Visibility = Visibility.Visible;
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
            Storyboard sb2 = FindResource("FadeOut") as Storyboard;
            Storyboard.SetTarget(sb2, this);
            sb2.Begin();
            //this.Visibility = Visibility.Hidden;
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

