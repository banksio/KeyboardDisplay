using System.Windows.Forms;

namespace KeyboardDisplay
{
    interface IYourForm1
    {
        void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon);
    }
}