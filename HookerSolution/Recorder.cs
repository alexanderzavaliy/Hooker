using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

using Gma.UserActivityMonitor;

namespace Hooker
{
    class Recorder
    {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);
        public enum CurrentApplicationState
        {
            Launched = 0,
            NotLaunched = 1
        }
        public delegate void OnStopRecordingDelegate();
        public GlobalEventProvider globalEventProvider;

        private const string COMMENT = "Comment";
        private const string MOUSE_MOVE = "MouseMove";
        private const string MOUSE_DOWN = "MouseDown";
        private const string MOUSE_DOUBLE = "MouseDouble";
        private const string KEY_DOWN = "KeyDown";
        private const string KEY_UP = "KeyUp";
       
        private string _applicationPath;
        private string _applicationProcessName;
        private Process _processToRecord;
        private string _recorderLogFilePath;
        private List<Keys> _keysDownList;
        private OnStopRecordingDelegate _onStopRecordingDelegate;
        
        public Recorder(string applicationPath, string applicationProcessName, string recorderLogFilePath, OnStopRecordingDelegate onStopRecordingDelegate)
        {
            _applicationPath = applicationPath;
            _applicationProcessName = applicationProcessName;
            _recorderLogFilePath = recorderLogFilePath;
            _onStopRecordingDelegate = onStopRecordingDelegate;
            globalEventProvider = new Gma.UserActivityMonitor.GlobalEventProvider();
        }
        
        public void StartRecording(CurrentApplicationState state)
        {
            if (state == CurrentApplicationState.NotLaunched)
            {
                _processToRecord = StartProcessToRecord(_applicationPath);
            }

            SetForegroundWindow(_applicationProcessName);
            DeleteRecorderLog(_recorderLogFilePath);
            _keysDownList = new List<Keys>();

            SubsribeGlobalEventProvider(globalEventProvider);
        }

        private Process StartProcessToRecord(string applicationPath)
        {
            _processToRecord = Process.Start(applicationPath);
            _processToRecord.EnableRaisingEvents = true;
            _processToRecord.Exited += new EventHandler(OnProcessToRecordExited);
            return _processToRecord;
        }

        private void SetForegroundWindow(string applicationProcessName)
        {
            Process[] processes = Process.GetProcessesByName(applicationProcessName);
            if (processes.Length > 0)
            {
                Console.WriteLine("Not found process with name = {0}", applicationProcessName);
                try
                {
                    IntPtr hwnd = processes[0].MainWindowHandle;
                    SetForegroundWindow(hwnd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception while SetForegroundWindow(string applicationProcessName)" + ex.Message);
                }
            }
        }

        private void DeleteRecorderLog(string recorderLogFilePath)
        {
            try
            {
                File.Delete(recorderLogFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while deleting " + recorderLogFilePath + " file");
            }
        }

        private void StopRecording()
        {
            try
            {
                _processToRecord.Kill();
            }
            catch (Exception ex)
            { 

            }
        }

        private void OnProcessToRecordExited(object sender, EventArgs e)
        {
            Process _exitedProcess = (Process)sender;
            if (_exitedProcess.Equals(_processToRecord))
            {
                UnSubsribeGlobalEventProvider(globalEventProvider);
                globalEventProvider.Dispose();
                Console.WriteLine("process killed");
            }
            _onStopRecordingDelegate();
        }

        private void SubsribeGlobalEventProvider(Gma.UserActivityMonitor.GlobalEventProvider provider)
        {
            provider.MouseDown += new MouseEventHandler(HookManager_MouseDown);
            provider.MouseMove += new MouseEventHandler(HookManager_MouseMove);
            provider.KeyDown += new KeyEventHandler(HookManager_KeyDown);
            provider.KeyUp += new KeyEventHandler(HookManager_KeyUp);
        }

        private void UnSubsribeGlobalEventProvider(Gma.UserActivityMonitor.GlobalEventProvider provider)
        {
            provider.MouseDown -= new MouseEventHandler(HookManager_MouseDown);
            provider.MouseMove -= new MouseEventHandler(HookManager_MouseMove);
            provider.KeyDown -= new KeyEventHandler(HookManager_KeyDown);
            provider.KeyUp -= new KeyEventHandler(HookManager_KeyUp);
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key pressed");
            if (!_keysDownList.Contains(e.KeyCode))
            {
                _keysDownList.Add(e.KeyCode);

                if (ShowRecordingControlIfKeysPressed(Keys.LShiftKey, Keys.Z))
                {

                }
                foreach (Keys k in _keysDownList)
                {
                    Debug.Write(k.ToString().ToLower() + " ");
                }
                Debug.WriteLine("");
                
                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(_recorderLogFilePath, true))
                {
                    wr.Write(KEY_DOWN + " ");

                    if (_keysDownList.Contains(Keys.LShiftKey) ||
                        _keysDownList.Contains(Keys.RShiftKey) ||
                        _keysDownList.Contains(Keys.LControlKey) ||
                        _keysDownList.Contains(Keys.RControlKey) ||
                        _keysDownList.Contains(Keys.Alt) ||
                        _keysDownList.Contains(Keys.LMenu))
                        {
                            foreach (Keys k in _keysDownList)
                            {
                                wr.Write(k.ToString().ToLower() + " ");
                            }
                        }
                        else
                        {
                            wr.Write(_keysDownList[_keysDownList.Count - 1].ToString().ToLower() + " ");
                        }
                    wr.WriteLine("");
                }
            }
        }

        private bool ShowRecordingControlIfKeysPressed(Keys keyOne, Keys KeyTwo)
        {
            if (_keysDownList.Contains(keyOne) && _keysDownList.Contains(KeyTwo))
            {
                _keysDownList.Remove(keyOne);
                _keysDownList.Remove(KeyTwo);

                UnSubsribeGlobalEventProvider(globalEventProvider);

                string commentBody = null;
                DialogResult dialogResult = CommentsDialog.Show("Recording Control", "Type your comment here", ref commentBody);
                if (dialogResult == DialogResult.OK)
                {
                    using (System.IO.StreamWriter wr = new System.IO.StreamWriter(_recorderLogFilePath, true))
                    {
                        wr.Write(COMMENT + " ");
                        wr.Write(commentBody);
                        wr.WriteLine("");
                    }
                    SubsribeGlobalEventProvider(globalEventProvider);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    StopRecording();
                }
                return true;
            }
            return false;
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = _keysDownList.Count - 1; i >= 0; i--)
            {
                if (_keysDownList[i].Equals(e.KeyCode))
                {
                    _keysDownList.Remove(_keysDownList[i]);
                    break;
                }
            }
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textBoxLog.AppendText(string.Format("KeyPress - {0}\n", e.KeyChar));
            //textBoxLog.ScrollToCaret();
        }


        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            //labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(_recorderLogFilePath, true))
            {
                wr.WriteLine(MOUSE_MOVE + " " + e.X + " " + e.Y);
            }
        }

        private void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            //textBoxLog.AppendText(string.Format("MouseClick - {0}\n", e.Button));
            //textBoxLog.ScrollToCaret();
        }
        
        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            //textBoxLog.AppendText(string.Format("MouseUp - {0}\n", e.Button));
            //textBoxLog.ScrollToCaret();
        }
        
        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            //textBoxLog.AppendText(string.Format("MouseDown - {0}\n", e.Button));
            //textBoxLog.ScrollToCaret();
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(_recorderLogFilePath, true))
            {
                wr.WriteLine(MOUSE_DOWN + " " + e.Location.X + " " + e.Location.Y + " " + e.Button);
            }
        }
        
        private void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //textBoxLog.AppendText(string.Format("MouseDoubleClick - {0}\n", e.Button));
            //textBoxLog.ScrollToCaret();
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(_recorderLogFilePath, true))
            {
                wr.WriteLine(MOUSE_DOUBLE + " " + e.Location.X + " " + e.Location.Y + " " + e.Button);
            }
        }


        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            //labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }


    }
}
