﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace KeyboardDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public bool startUp = true;
        UpdateManager updater;
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

        public MainWindow()
        {
            InitializeComponent();
            
            //DispatcherTimer fadeDelay = new DispatcherTimer();
            //fadeDelay.Interval = new TimeSpan(0,0,5);

            _hook = new KeyboardHook();
            _hook.KeyUp += new KeyboardHook.HookEventHandler(OnHookKeyUp);

            //check for updates last, it is least important
            updater = new UpdateManager();
            updater.GetUpdateInfo();

        }

        void OnHookKeyUp(object sender, HookEventArgs e)
        {
            checkKeyLocks();
        }

        private void checkKeyLocks()
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock))
            {
                if (Functions.ChangeStoredLock(Keys.CapsLock, true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.TypeLabelText(Keys.CapsLock);
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.CapsLock)))
            {
                if (Functions.ChangeStoredLock(Keys.CapsLock, false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.TypeLabelText(Keys.CapsLock);
                    ChangeDisplay();
                }
            }
            if (Keyboard.IsKeyToggled(Key.NumLock))
            {
                if (Functions.ChangeStoredLock(Keys.NumLock, true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.TypeLabelText(Keys.NumLock);
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.NumLock)))
            {
                if (Functions.ChangeStoredLock(Keys.NumLock, false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.TypeLabelText(Keys.NumLock);
                    ChangeDisplay();
                }

            }
            if (Keyboard.IsKeyToggled(Key.Scroll))
            {
                if (Functions.ChangeStoredLock(Keys.Scroll, true))
                {
                    label1.Content = "On";
                    typeLabel.Content = Functions.TypeLabelText(Keys.Scroll);
                    ChangeDisplay();
                }
            }
            else if (!(Keyboard.IsKeyToggled(Key.Scroll)))
            {
                if (Functions.ChangeStoredLock(Keys.Scroll, false))
                {
                    label1.Content = "Off";
                    typeLabel.Content = Functions.TypeLabelText(Keys.Scroll);
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
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
            startUp = false;
        }
    }
}

