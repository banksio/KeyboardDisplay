using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardDisplay
{
    public class Functions
    {
        public static string StartupRegistryKeyName = "KbdDispStart";

        public static string TypeLabelText(string type)
        {
            switch (type)
            {
                case "CapsLock":
                    return "Caps Lock";
                case "NumLock":
                    return "Num Lock";
                case "ScrLock":
                    return "Scroll Lock";
                default:
                    return "Unknown";
            }
        }

        public static bool ChangeStoredLock(string type, bool locked)
        {
            switch (type) {
                case "CapsLock":
                    if (locked)
                    {
                        CapsLock.prevstate = CapsLock.curstate;
                        CapsLock.curstate = "on";
                    } else
                    {
                        CapsLock.prevstate = CapsLock.curstate;
                        CapsLock.curstate = "off";
                    }
                    if (CapsLock.prevstate != CapsLock.curstate)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                case "NumLock":
                    if (locked)
                    {
                        NumLock.prevstate = NumLock.curstate;
                        NumLock.curstate = "on";
                    }
                    else
                    {
                        NumLock.prevstate = NumLock.curstate;
                        NumLock.curstate = "off";
                    }
                    if (NumLock.prevstate != NumLock.curstate)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "ScrLock":
                    if (locked)
                    {
                        ScrLock.prevstate = ScrLock.curstate;
                        ScrLock.curstate = "on";
                    }
                    else
                    {
                        ScrLock.prevstate = ScrLock.curstate;
                        ScrLock.curstate = "off";
                    }
                    if (ScrLock.prevstate != ScrLock.curstate)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        public static bool GetStartupRegistryKeyStatus(string scope)
        {
            RegistryKey rk;

            // Opening the registry key
            switch (scope)
            {
                case "currentUser":
                    rk = Registry.CurrentUser;
                    break;
                case "localMachine":
                    rk = Registry.LocalMachine;
                    break;
                default:
                    return false;
            }

            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return false;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    if ((string)sk1.GetValue("KbdDispStart") == null)
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    MessageBox.Show(e.Message);
                    return false;
                }
            }
        }

        public static void SetStartupRegistryKeyStatus(string scope, bool delete = false)
        {
            RegistryKey rk;

            // Opening the registry key
            switch (scope)
            {
                case "currentUser":
                    rk = Registry.CurrentUser;
                    break;
                case "localMachine":
                    rk = Registry.LocalMachine;
                    break;
                default:
                    return;
            }

            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run",true);

            if (delete)
            {
                try
                {
                    sk1.DeleteValue(StartupRegistryKeyName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            } else
            {
                try
                {
                    // Save the value
                    sk1.SetValue(StartupRegistryKeyName, "\"" + System.Reflection.Assembly.GetEntryAssembly().Location + "\"");
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
            
        }
        
    }

    public partial class MainWindow
    {
        
    }
}