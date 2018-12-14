using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KeyboardDisplay
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //set checkbox
            startupCheckbox.IsChecked = Functions.GetStartupRegistryKeyStatus("currentUser");
            startupCheckbox_AllUsers.IsChecked = Functions.GetStartupRegistryKeyStatus("localMachine");
        }

        private void StartupCheckbox_Click(object sender, RoutedEventArgs e)
        {
            //logic for logon startup here
            if ((bool)startupCheckbox.IsChecked)
            {
                Functions.SetStartupRegistryKeyStatus("currentUser");
            }
            else
            {
                Functions.SetStartupRegistryKeyStatus("currentUser", true);
            }
        }

        private void StartupCheckbox_AllUsers_Click(object sender, RoutedEventArgs e)
        {
            //logic for logon startup here
            if ((bool)startupCheckbox.IsChecked)
            {
                Functions.SetStartupRegistryKeyStatus("localMachine");
            }
            else
            {
                Functions.SetStartupRegistryKeyStatus("localMachine", true);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
