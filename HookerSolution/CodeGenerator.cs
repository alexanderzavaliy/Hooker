using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Hooker
{
    public static class CodeGenerator
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private const string COMMENT = "Comment";
        private const string MOUSE_MOVE = "MouseMove";
        private const string MOUSE_DOWN = "MouseDown";
        private const string KEY_DOWN = "KeyDown";
        private const string KEY_UP = "KeyUp";

        private const int THINK_TIME_BETWEEN_MOUSE_MOVES = 2000;
        private const int THINK_TIME_BETWEEN_MOUSE_DOWNS = 1500;
        private const int THINK_TIME_BETWEEN_KEY_DOWNS = 1200;

        private static Dictionary<string, string> _actionLogKeyToCodeLogKey = new Dictionary<string, string>()
        {
            {"d1", "1" },
            {"d2", "2" },
            {"d3", "3" },
            {"d4", "4" },
            {"d5", "5" },
            {"d6", "6" },
            {"d7", "7" },
            {"d8", "8" },
            {"d9", "9" },
            {"d0", "0" },
            {"back", "{BACKSPACE}" },
            {"delete", "{DELETE}" },
            {"return", "{ENTER}" },
            {"lshiftkey", "+" },
            {"rshiftkey", "+" },
            {"lcontrolkey", "^" },
            {"rcontrolkey", "^" },
            {"lmenu", "%" },
            {"rmenu", "%" },

            {"a", "a" },
            {"b", "b" },
            {"c", "c" },
            {"d", "d" },
            {"e", "e" },
            {"f", "f" },
            {"g", "g" },
            {"h", "h" },
            {"i", "i" },
            {"j", "j" },
            {"k", "k" },
            {"l", "l" },
            {"m", "m" },
            {"n", "n" },
            {"o", "o" },
            {"p", "p" },
            {"q", "q" },
            {"r", "r" },
            {"s", "s" },
            {"t", "t" },
            {"u", "u" },
            {"v", "v" },
            {"w", "w" },
            {"x", "x" },
            {"y", "y" },
            {"z", "z" },

            {"escape", "{ESC}" },
            {"space", " " },
            {"f1", "{F1}" },
            {"f2", "{F2}" },
            {"f3", "{F3}" },
            {"f4", "{F4}" },
            {"f5", "{F5}" },
            {"f6", "{F6}" },
            {"f7", "{F7}" },
            {"f8", "{F8}" },
            {"f9", "{F9}" },
            {"f10", "{F10}" },
            {"f11", "{F11}" },
            {"f12", "{F12}" },

            {"up", "{UP}" },
            {"down", "{DOWN}" },
            {"left", "{LEFT}" },
            {"right", "{RIGHT}" },

            {"tab", "{TAB}"},
            {"", ""},
            {"oemcomma", ","}
        };

        public static void PerformStandardCodeGeneration(string actionLogFilePath, string codeLogFilePath)
        {
            using (StreamReader sr = new StreamReader(actionLogFilePath))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine("line = " + line);
                    string[] parameters = line.Split(' ');

                    if (parameters[0].Equals(MOUSE_MOVE))
                    {
                        GenerateMouseMoves(codeLogFilePath, parameters, THINK_TIME_BETWEEN_MOUSE_MOVES);
                    }
                    else if (parameters[0].Equals(MOUSE_DOWN))
                    {
                        GenerateMouseDowns(codeLogFilePath, parameters, THINK_TIME_BETWEEN_MOUSE_DOWNS);
                    }
                    else if (parameters[0].Equals(KEY_DOWN))
                    {
                        GenerateKeyDowns(codeLogFilePath, parameters, THINK_TIME_BETWEEN_KEY_DOWNS);
                    }
                    else if (parameters[0].Equals(COMMENT))
                    {
                        GenerateComments(codeLogFilePath, parameters);
                    }
                }
            }
        }

        public static void PerformImprovedCodeGeneration(string actionLogFilePath, string codeLogFilePath)
        {
            try
            {
                File.Delete(codeLogFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while deleting " + codeLogFilePath + " file");
            }

            string[] _actionLogContent = null;
            _actionLogContent = File.ReadAllLines(actionLogFilePath);

            int i = 0;
            while (i < _actionLogContent.Length)
            {
                string line = _actionLogContent[i];
                string[] parameters = line.Split(' ');

                if (parameters[0].Equals(MOUSE_MOVE))
                {
                    int j = i + 1;
                    while (j < _actionLogContent.Length)
                    {
                        string nextLine = _actionLogContent[j];
                        string[] nextParameters = nextLine.Split(' ');

                        if (nextParameters[0].Equals(MOUSE_DOWN) || nextParameters[0].Equals(KEY_DOWN))
                        {
                            GenerateMouseMoves(codeLogFilePath, parameters, j * THINK_TIME_BETWEEN_MOUSE_MOVES);
                            i = j - 1;
                            break;
                        }
                        else j++;
                    }
                }
                else if (parameters[0].Equals(MOUSE_DOWN))
                {
                    GenerateMouseDowns(codeLogFilePath, parameters, THINK_TIME_BETWEEN_MOUSE_DOWNS);
                }
                else if (parameters[0].Equals(KEY_DOWN))
                {
                    GenerateKeyDowns(codeLogFilePath, parameters, THINK_TIME_BETWEEN_KEY_DOWNS);
                }
                else if (parameters[0].Equals(COMMENT))
                {
                    GenerateComments(codeLogFilePath, parameters);
                }
                i++;
            }
        }

        private static void GenerateMouseMoves(string codeLogFilePath, string[] parameters, int thinkTimeMilliseconds)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                sw.WriteLine("System.Windows.Forms.Cursor.Position = new System.Drawing.Point({0}, {1});", parameters[1], parameters[2]);
                sw.WriteLine("System.Threading.Thread.Sleep({0});", THINK_TIME_BETWEEN_MOUSE_MOVES);
            }
        }

        private static void GenerateMouseDowns(string codeLogFilePath, string[] parameters, int thinkTimeMilliseconds)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                sw.WriteLine("System.Windows.Forms.Cursor.Position = new System.Drawing.Point({0}, {1});", parameters[1], parameters[2]);
                sw.WriteLine("System.Threading.Thread.Sleep({0});", THINK_TIME_BETWEEN_MOUSE_DOWNS);
                sw.WriteLine("mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, {0}, {1}, 0, 0);", parameters[1], parameters[2]);
                sw.WriteLine("System.Threading.Thread.Sleep({0});", THINK_TIME_BETWEEN_MOUSE_DOWNS);
            }
        }

        private static void GenerateKeyDowns(string codeLogFilePath, string[] parameters, int thinkTimeMilliseconds)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                //TODO: GENERATE MULTIPLE KEY PRESSES, CURRENTLY ONLY ONE IS PROCESSED
                string keyToSend = null;
                try
                {
                    keyToSend = _actionLogKeyToCodeLogKey[parameters[1]];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception while processing KeyDown " + ex.Message + " " + parameters[1]);
                }
                if (parameters.Length <= 2)
                {
                    if (keyToSend != null)
                    {
                        sw.WriteLine("System.Windows.Forms.SendKeys.SendWait(\"{0}\");", keyToSend);
                        sw.WriteLine("System.Threading.Thread.Sleep({0});", THINK_TIME_BETWEEN_KEY_DOWNS);
                    }
                }
                else
                {
                    string key1 = null;
                    string key2 = null;
                    string key3 = null;

                    try
                    {
                        key1 = _actionLogKeyToCodeLogKey[parameters[1]];
                        key2 = _actionLogKeyToCodeLogKey[parameters[2]];
                        key3 = _actionLogKeyToCodeLogKey[parameters[3]];
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Exception while processing KeyDown 2 params " + "length = " + parameters.Length + " " + ex.Message + " : p0 = " + parameters[0] + " p1 = " + parameters[1] + " p2 = " + parameters[2]);
                    }
                    sw.WriteLine("System.Windows.Forms.SendKeys.SendWait(\"{0}{1}\");", key1, key2, key3);
                    sw.WriteLine("System.Threading.Thread.Sleep({0});", THINK_TIME_BETWEEN_KEY_DOWNS);
                }
            }
        }

        private static void GenerateComments(string codeLogFilePath, string[] parameters)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                StringBuilder sb = new StringBuilder();
                if (parameters.Length > 1)
                {
                    for (int i = 1; i < parameters.Length; i++)
                    {
                        sb.Append(parameters[i]);
                        sb.Append(" ");
                    }
                }
                sw.WriteLine("//-------------------------------------------------------------------");
                sw.WriteLine("//    " + sb.ToString());
                sw.WriteLine("//-------------------------------------------------------------------");
            }
        }
    }
}
