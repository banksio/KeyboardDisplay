using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardDisplay
{
    public class Functions
    {
        public static string typeLabelText(string type)
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

        public static bool changeStoredLock(string type, bool locked)
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

    }

    public partial class MainWindow
    {
        
    }
}