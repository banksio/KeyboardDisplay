﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace KeyboardDisplay
{
    public class UpdateManager
{
        private static bool _updateAvailable;
        private static WebClient wc;
        //private static readonly IYourForm1 interf = new MainWindow();

        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static string savepath = Path.Combine(path, "Keyboard Display\\Temp");
        private static string filepath = Path.Combine(savepath, "updateSetup.exe");
        private static string version;

        private static NotifyIcon MainNI;

        public static bool UpdateAvailable
        {
            get => _updateAvailable;
            set => _updateAvailable = value;
        }

        public UpdateManager()
        {
            MainNI = CreateNotifyIcon();
            MainNI.BalloonTipClicked += new EventHandler(DoUpdate);
            MainNI.ContextMenu = CreateContextMenu();
            wc = new WebClient();
            wc.DownloadFileCompleted += UpdateDownloaded;
            UpdateAvailable = false;
        }

        public static NotifyIcon CreateNotifyIcon()
        {
            //create new NotifyIcon
            NotifyIcon newNI = new NotifyIcon();
            //set icon to icon saved in resources
            newNI.Icon = Properties.Resources.KBDDisp;
            //make NotifyIcon visible
            newNI.Visible = true;
            //add tooltip text
            newNI.Text = "Keyboard Display is running.";

            return newNI;
        }

        static void NotifyUpdate()
        {
            MainNI.ShowBalloonTip(1, "Keyboard Display Update", "Version " + version + " has been downloaded. Tap or click here to install it.", ToolTipIcon.Info);
        }

        public ContextMenu CreateContextMenu()
        {
            // create new ContextMenu
            ContextMenu newCM = new ContextMenu();
            newCM.MenuItems.Add("Settings");
            newCM.MenuItems.Add("Exit");
            return newCM;
        }

        private void MenuItem_Click(object sender, System.EventArgs e)
            {
                MainNI.Dispose();
                Application.Current.Shutdown();
            }

        private void SettingsMenuItem_Click(object sender, System.EventArgs e)
        {
            Window1 settings = new Window1();
            settings.Show();
        }

        public void GetUpdateInfo()
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

        private static byte[] GetURLContents(string url)
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

        private static void ProcessURLResponse(string url, byte[] content)
        {
            var bytes = content.Length;
            var contentString = Regex.Replace(Encoding.UTF8.GetString(content), @"\t|\n|\r", "!");
            var contentStringSplit = contentString.Split(Convert.ToChar("!"));
            version = contentStringSplit[0];

            if (int.Parse(version.Replace(@".", "")) > int.Parse(Properties.Resources.version.Replace(@".", "")))
            {
                UpdateAvailable = true;
                DownloadUpdate(contentStringSplit[1]);
            }
        }

        private static void DownloadUpdate(string updateUrl)
        {
            if (UpdateAvailable)
            {
                try
                {
                    if (!Directory.Exists(savepath))
                    {
                        Directory.CreateDirectory(savepath);
                    }
                    else
                    {
                        if (!File.Exists(filepath))
                        {
                            DirectoryInfo di = new DirectoryInfo(savepath);

                            foreach (FileInfo fi in di.GetFiles())
                            {

                                fi.Delete();
                            }
                        }
                        else
                        {
                            NotifyUpdate();
                            return;
                        }
                    }

                    wc.DownloadFileAsync(new Uri(updateUrl), filepath, filepath);

                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException) { }
                    else
                    {
                        MessageBox.Show(ex.Message, "Updater Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Update available to download.", "Updater Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoUpdate(object sender, System.EventArgs e)
        {
            try
            {
                Process.Start(filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Updater Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            Application.Current.Shutdown();
        }


        private void UpdateDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled || e.Error != null)
            {
                NotifyUpdate();
            } else
            {
                MessageBox.Show(e.Error.Message, "Updater Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                
        }

    }
}
