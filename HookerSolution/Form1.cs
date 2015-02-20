using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace Hooker
{
    public partial class Form1 : Form
    {
        private TextBox _applicationPathTextBox;
        private TextBox _applicationProcessNameTextBox;
        private Button _startRecordingButton;
        private CheckBox _launchApplicationCheckBox;

        private Recorder _recorder;
        private CodeGenerator _codeGenerator;
        string _recorderLogFilePath = "recorderLog.txt";
        string _codeGeneratorLogFilePath = "codeGeneratorLog.txt";

        public Form1()
        {
            InitializeComponent();

            _applicationPathTextBox = new TextBox();
            _applicationPathTextBox.Text = @"C:\Windows\System32\Notepad.exe";
            _applicationPathTextBox.Location = new Point(10, 10);
            _applicationPathTextBox.Size = new Size(300, 20);
            this.Controls.Add(_applicationPathTextBox);

            _applicationProcessNameTextBox = new TextBox();
            _applicationProcessNameTextBox.Text = @"notepad";
            _applicationProcessNameTextBox.Location = new Point(10, 40);
            _applicationProcessNameTextBox.Size = new Size(300, 20);
            this.Controls.Add(_applicationProcessNameTextBox);

            _startRecordingButton = new Button();
            _startRecordingButton.Text = "Record";
            _startRecordingButton.Location = new Point(10, 80);
            _startRecordingButton.Size = new Size(80, 30);
            _startRecordingButton.Click += new EventHandler(OnStartRecordingButtonClick);
            this.Controls.Add(_startRecordingButton);

            _launchApplicationCheckBox = new CheckBox();
            _launchApplicationCheckBox.Text = "Launch Application before recording";
            _launchApplicationCheckBox.Location = new Point(100, 85);
            _launchApplicationCheckBox.Size = new Size(80, 30);
            _launchApplicationCheckBox.Checked = false;
            this.Controls.Add(_launchApplicationCheckBox);

        }

        private void OnStartRecordingButtonClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

            _recorder = new Recorder(_applicationPathTextBox.Text, _applicationProcessNameTextBox.Text, _recorderLogFilePath, new Recorder.OnStopRecordingDelegate(OnStopRecording));

            if (_launchApplicationCheckBox.Checked == true)
                _recorder.StartRecording(Recorder.CurrentApplicationState.NotLaunched);
            else
                _recorder.StartRecording(Recorder.CurrentApplicationState.Launched);
        }

        private void OnStopRecording()
        {
            Debug.WriteLine("Recording stopped");

            this.Invoke((MethodInvoker)delegate
            {
                this.WindowState = FormWindowState.Normal;
                _startRecordingButton.Text = "Record";
                _startRecordingButton.Enabled = true;
                //CodeGenerator.PerformImprovedCodeGeneration(ACTION_LOG_FILE_PATH, CODE_LOG_FILE_PATH);
                MessageBox.Show("Recording stopped", "Ok");
            });

            _codeGenerator = new CodeGenerator();
            _codeGenerator.PerformStandardCodeGeneration(_recorderLogFilePath, _codeGeneratorLogFilePath);
        }










   
    }
}
