using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace Hooker
{
    public class Replayer
    {
	static void Main()
	{

	}

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

	public Replayer()
	{

	}

        public void ExecuteRecordedCode()
        { 
                //GENERATED_CODE_STARTS_AFTER_THIS_LINE		
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(470, 288);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(470, 288);
                System.Threading.Thread.Sleep(500);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 470, 288, 0, 0);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(470, 283);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("");
                System.Threading.Thread.Sleep(500);
	}
    }
}
