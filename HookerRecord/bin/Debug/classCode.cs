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
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(80, 153);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(82, 162);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(88, 176);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(97, 199);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(110, 228);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(137, 268);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(184, 330);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(238, 384);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(291, 433);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(353, 477);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(412, 508);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(460, 530);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(507, 552);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(546, 563);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(569, 572);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(591, 579);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(606, 583);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(615, 585);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(619, 585);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(623, 587);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(626, 587);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(630, 587);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(635, 588);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(638, 588);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(642, 590);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(645, 590);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(646, 590);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(648, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(650, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(651, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(652, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(653, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(654, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(655, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(657, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(659, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(662, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(666, 591);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(671, 592);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(675, 592);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(681, 592);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(689, 592);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(698, 592);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(712, 590);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(728, 590);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(745, 588);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(762, 583);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(782, 579);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(796, 577);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(813, 575);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(822, 571);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(834, 567);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(840, 566);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(844, 564);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(849, 563);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(850, 561);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(853, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("+");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(855, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(860, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(864, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(868, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(873, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(875, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(878, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(879, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(880, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(882, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(883, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("+t");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("h");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("i");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("s");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait(" ");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("i");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("s");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait(" ");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("h");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("g");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("r");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("e");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("a");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("t");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait(" ");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("e");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("x");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("a");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("m");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(885, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(891, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(896, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(902, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(906, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(909, 560);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(912, 561);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(913, 561);
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("p");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("l");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("e");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait(",");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait(" ");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("y");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("e");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("s");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("%");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("%{F4}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
                System.Threading.Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                System.Threading.Thread.Sleep(500);
	}
    }
}
