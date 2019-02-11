using System.Windows;

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
            alwaysOnCheckbox.IsChecked = Properties.Settings.Default.alwaysOn;
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

        private void alwaysOnCheckbox_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.alwaysOn = alwaysOnCheckbox.IsChecked.Value;
            Properties.Settings.Default.Save();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
