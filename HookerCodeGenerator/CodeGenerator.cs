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
    public class CodeGenerator
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
        private const string THINK_TIME_BETWEEN_MOUSE_MOVES_KEY = "THINK_TIME_BETWEEN_MOUSE_MOVES";
        private const string THINK_TIME_BETWEEN_MOUSE_DOWNS_KEY = "THINK_TIME_BETWEEN_MOUSE_DOWNS";
        private const string THINK_TIME_BETWEEN_KEY_DOWNS_KEY = "THINK_TIME_BETWEEN_KEY_DOWNS";
        private const string CLASS_CODE_TEMPLATE_FILE_PATH_KEY = "CLASS_CODE_TEMPLATE_FILE_PATH";
        private const string CLASS_CODE_FILE_PATH_KEY = "CLASS_CODE_FILE_PATH";
        private const string GENERATED_CODE_STARTS_AFTER_THIS_LINE_KEY = "GENERATED_CODE_STARTS_AFTER_THIS_LINE";
        
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
            //{"lwin", "^{ESC}"}, //doesn't work directly, but may be overcomed using native windows calls, or third-party libs solutions based on native windows calls. Not implemented currently

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
            {"oemcomma", ","},
            {"oemquestion", "{/}"},
            {"oemtilde", "{~}"},
            {"oemopenbrackets", "{[}"},
            {"oem5", "{\\\\}"}, //1st "\" is escape-symbol, 2d is symbol we'd like to type, 3d and 4th are required to write "\\" to classCode file
            {"oem6", "{]}"}
        };

        private static Dictionary<string, string> _lineBreakerKeys = new Dictionary<string, string>()
        {
            {"back", "" },
            {"delete", "" },
            {"return", "" },
            {"lshiftkey", "" },
            {"rshiftkey", "" },
            {"lcontrolkey", "^" },
            {"rcontrolkey", "^" },
            {"lmenu", "" },
            {"rmenu", "" },
            //{"lwin", "^{ESC}"}, //doesn't work directly, but may be overcomed using native windows calls, or third-party libs solutions based on native windows calls. Not implemented currently

 
            {"escape", "" },
            {"space", "" },
            {"f1", "" },
            {"f2", "" },
            {"f3", "" },
            {"f4", "" },
            {"f5", "" },
            {"f6", "" },
            {"f7", "" },
            {"f8", "" },
            {"f9", "" },
            {"f10", "" },
            {"f11", "" },
            {"f12", "" },

            {"up", "" },
            {"down", "" },
            {"left", "" },
            {"right", "" },

            {"tab", ""},
        };

        public CodeGenerator()
        {
        
        }

        public void CreateOptimizedRecorderLog(string recorderLogFilePath, string recorderLogOptimizedFilePath)
        {
            string[] lines = File.ReadAllLines(recorderLogFilePath);
            List<string> linesToWrite = new List<string>();
            
            string currentLine;
            string nextLine;
           
            StringBuilder sb = new StringBuilder("");
            bool sbWasAppended = false;

            for(int i = 0; i < lines.Length - 1; i++)
            {
                currentLine = lines[i];
                nextLine = lines[i + 1];
                if (!sbWasAppended)
                    sb = new StringBuilder(currentLine);

                if (currentLine.StartsWith(MOUSE_MOVE) && !nextLine.StartsWith(MOUSE_MOVE))
                {
                    linesToWrite.Add(currentLine);
                }

                if (currentLine.StartsWith(MOUSE_DOWN))
                {
                    linesToWrite.Add(currentLine);
                }
                
                if (currentLine.StartsWith(KEY_DOWN))
                {
                    if (ContainsLineBreaker(nextLine))
                    {
                        linesToWrite.Add(sb.ToString());
                        sbWasAppended = false;
                    }
                    else
                    {
                        string[] parameters = nextLine.Split(' ');
                        
                        for (int j = 1; j < parameters.Length; j++)
                        {
                            if (!sb[sb.Length-1].Equals(' ')) sb.Append(" ");
                            sb.Append(parameters[j]);
                        }
                        sbWasAppended = true;
                    }
                }
            }
            File.WriteAllLines(recorderLogOptimizedFilePath, linesToWrite.ToArray());
        }

        private bool ContainsLineBreaker(string line)
        {
            string[] parameters = line.Split(' ');

            for (int j = 0; j < parameters.Length; j++)
            {
                if (_lineBreakerKeys.Keys.Contains(parameters[j]))
                {
                    return true;
                }
            }
            return false;
        }

        public void PerformStandardCodeGeneration(string recorderLogFilePath, string codeGeneratorConfigurationFilePath, string codeGeneratorLogFilePath, string classCodeTemplateFilePath, string classCodeFilePath)
        {
            Dictionary<string, string> configurationArgs = ReadConfigurationFromFile(codeGeneratorConfigurationFilePath);

            string generatedCodeStartsAfterThisLine = configurationArgs[GENERATED_CODE_STARTS_AFTER_THIS_LINE_KEY];
            int thinkTimeBetweenMouseMoves = Int32.Parse(configurationArgs[THINK_TIME_BETWEEN_KEY_DOWNS_KEY]);
            int thinkTimeBetweenMouseDowns = Int32.Parse(configurationArgs[THINK_TIME_BETWEEN_MOUSE_DOWNS_KEY]);
            int thinkTimeBetweenKeyDowns = Int32.Parse(configurationArgs[THINK_TIME_BETWEEN_KEY_DOWNS_KEY]);

            using (StreamReader sr = new StreamReader(recorderLogFilePath))
            {
                DeleteCodeLog(codeGeneratorLogFilePath);

                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine("line = " + line);
                    string[] parameters = line.Split(' ');

                    if (parameters[0].Equals(MOUSE_MOVE))
                    {
                        GenerateMouseMoves(codeGeneratorLogFilePath, parameters, thinkTimeBetweenMouseMoves);
                    }
                    else if (parameters[0].Equals(MOUSE_DOWN))
                    {
                        GenerateMouseDowns(codeGeneratorLogFilePath, parameters, thinkTimeBetweenMouseDowns);
                    }
                    else if (parameters[0].Equals(KEY_DOWN))
                    {
                        GenerateKeyDowns(codeGeneratorLogFilePath, parameters, thinkTimeBetweenKeyDowns);
                    }
                    else if (parameters[0].Equals(COMMENT))
                    {
                        GenerateComments(codeGeneratorLogFilePath, parameters);
                    }
                }
            }

            PerformClassCodeGeneration(codeGeneratorLogFilePath, classCodeTemplateFilePath, generatedCodeStartsAfterThisLine, classCodeFilePath);
        }

        private void PerformClassCodeGeneration(string codeGeneratorLogFilePath, string classCodeTemplateFilePath, string generatedCodeStartsAfterThisLine, string classCodeFilePath)
        {
            string[] codeGeneratorLines = File.ReadAllLines(codeGeneratorLogFilePath);
            List<string> classCodeLines = File.ReadAllLines(classCodeTemplateFilePath).ToList();

            int stringAfterWhichGeneratedCodeShouldBeInsertedIndex = 0;
            foreach(string classCodeLine in classCodeLines)
            {
                stringAfterWhichGeneratedCodeShouldBeInsertedIndex++;
                if (classCodeLine.Contains(generatedCodeStartsAfterThisLine))
                {
                    int offsetForCodeLinesInsertion = classCodeLine.IndexOf(generatedCodeStartsAfterThisLine);
                    for (int i = 0; i < codeGeneratorLines.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder(codeGeneratorLines[i]);
                        sb.Insert(0, " ", offsetForCodeLinesInsertion);
                        codeGeneratorLines[i] = sb.ToString();
                    }
                    break;
                }
            }

            for(int i = codeGeneratorLines.Length - 1; i >= 0 ; i--)
            {
                classCodeLines.Insert(stringAfterWhichGeneratedCodeShouldBeInsertedIndex, codeGeneratorLines[i]);
            }

            File.WriteAllLines(classCodeFilePath, classCodeLines.ToArray());
        }

        private void DeleteCodeLog(string codeLogFilePath)
        {
            try
            {
                File.Delete(codeLogFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while deleting " + codeLogFilePath + " file" + " ex = " + ex);
            }
        }

        private Dictionary<string, string> ReadConfigurationFromFile(string codeGeneratorConfigurationFilePath)
        {
            Dictionary<string, string> configurationArgs = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(codeGeneratorConfigurationFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] temp = line.Split(' ');
                    if (temp.Length >= 2)
                    {
                        configurationArgs.Add(temp[0], temp[1]); 
                    }
                }
            }
            return configurationArgs;
        }

        private void GenerateMouseMoves(string codeLogFilePath, string[] parameters, int thinkTimeMilliseconds)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                sw.WriteLine("System.Windows.Forms.Cursor.Position = new System.Drawing.Point({0}, {1});", parameters[1], parameters[2]);
                sw.WriteLine("System.Threading.Thread.Sleep({0});", thinkTimeMilliseconds);
            }
        }

        private void GenerateMouseDowns(string codeLogFilePath, string[] parameters, int thinkTimeMilliseconds)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                sw.WriteLine("System.Windows.Forms.Cursor.Position = new System.Drawing.Point({0}, {1});", parameters[1], parameters[2]);
                sw.WriteLine("System.Threading.Thread.Sleep({0});", thinkTimeMilliseconds);
                
                if (parameters[3] == "Left")
                {
                    sw.WriteLine("mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, {0}, {1}, 0, 0);", parameters[1], parameters[2]);
                    sw.WriteLine("System.Threading.Thread.Sleep({0});", thinkTimeMilliseconds);
                }
                else if (parameters[3] == "Right")
                {
                    sw.WriteLine("mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, {0}, {1}, 0, 0);", parameters[1], parameters[2]);
                    sw.WriteLine("System.Threading.Thread.Sleep({0});", thinkTimeMilliseconds);
                }
            }
        }

        private void GenerateKeyDowns(string codeLogFilePath, string[] parameters, int thinkTimeMilliseconds)
        {
            using (StreamWriter sw = new StreamWriter(codeLogFilePath, true))
            {
                StringBuilder keys = new StringBuilder();
                for(int i = 1; i < parameters.Length; i++)
                {
                    string key;
                    try 
                    {
                        _actionLogKeyToCodeLogKey.TryGetValue(parameters[i], out key);
                        keys.Append(key);
                    }
                    catch(Exception ex)
                    {

                    }
                }

                sw.WriteLine("System.Windows.Forms.SendKeys.SendWait(\"{0}\");", keys.ToString());
                sw.WriteLine("System.Threading.Thread.Sleep({0});", thinkTimeMilliseconds);
            }
        }

        private void GenerateComments(string codeLogFilePath, string[] parameters)
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
