using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Hooker
{
    public partial class CodeGeneratorForm : Form
    {
        private const int TEXTBOX_WIDTH = 350;
        private const int TEXTBOX_HEIGHT = 30;
        private const int BUTTON_WIDTH = 100;
        private const int BUTTON_HEIGHT = 20;

        private Label _recorderLogFilePathLabel;
        private Label _codeGeneratorLogFilePathLabel;
        private Label _codeGeneratorConfigurationFilePathLabel;
        private Label _classCodeTemplateFilePathLabel;
        private Label _classCodeFilePathLabel;
        
        private TextBox _recorderLogFilePathTextBox;
        private TextBox _codeGeneratorLogFilePathTextBox;
        private TextBox _codeGeneratorConfigurationFilePathTextBox;
        private TextBox _classCodeTemplateFilePathTextBox;
        private TextBox _classCodeFilePathTextBox;

        private Button _browseRecorderLogFileButton;
        private Button _browseCodeGeneratorConfigurationFileButton;
        private Button _saveCodeGeneratorLogFileButton;
        private Button _browseClassCodeTemplateFileButton;
        private Button _saveClassCodeFileButton;
        private Button _generateClassCodeFileButton;

        public CodeGeneratorForm()
        {
            InitializeComponent();

            this.Text = "HookerCodeGenerator";
            this.Size = new Size(500, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            //recorderLog
            _recorderLogFilePathLabel = new Label();
            _recorderLogFilePathLabel.Location = new Point(10, 10);
            _recorderLogFilePathLabel.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _recorderLogFilePathLabel.Text = "recorderLogFilePath";
            this.Controls.Add(_recorderLogFilePathLabel);

            _recorderLogFilePathTextBox = new TextBox();
            _recorderLogFilePathTextBox.Location = new Point(10, 40);
            _recorderLogFilePathTextBox.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _recorderLogFilePathTextBox.Text = @"..\..\..\HookerRecord\bin\Debug\recorderLogOptimized.txt";
            this.Controls.Add(_recorderLogFilePathTextBox);

            _browseRecorderLogFileButton = new Button();
            _browseRecorderLogFileButton.Location = new Point(380, 39);
            _browseRecorderLogFileButton.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            _browseRecorderLogFileButton.Text = "Browse";
            _browseRecorderLogFileButton.Click += new EventHandler(OnBrowseButtonClick);
            this.Controls.Add(_browseRecorderLogFileButton);

            //codeGeneratorConfigurationLog
            _codeGeneratorConfigurationFilePathLabel = new Label();
            _codeGeneratorConfigurationFilePathLabel.Location = new Point(10, 80);
            _codeGeneratorConfigurationFilePathLabel.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _codeGeneratorConfigurationFilePathLabel.Text = "codeGeneratorConfigurationFilePath";
            this.Controls.Add(_codeGeneratorConfigurationFilePathLabel);

            _browseCodeGeneratorConfigurationFileButton = new Button();
            _browseCodeGeneratorConfigurationFileButton.Location = new Point(380, 110);
            _browseCodeGeneratorConfigurationFileButton.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            _browseCodeGeneratorConfigurationFileButton.Text = "Browse";
            _browseCodeGeneratorConfigurationFileButton.Click += new EventHandler(OnBrowseButtonClick);
            this.Controls.Add(_browseCodeGeneratorConfigurationFileButton);

            _codeGeneratorConfigurationFilePathTextBox = new TextBox();
            _codeGeneratorConfigurationFilePathTextBox.Location = new Point(10, 110);
            _codeGeneratorConfigurationFilePathTextBox.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _codeGeneratorConfigurationFilePathTextBox.Text = @"..\..\..\HookerRecord\bin\Debug\codeGeneratorConfiguration.txt";
            this.Controls.Add(_codeGeneratorConfigurationFilePathTextBox);

            
            //codeGeneratorLog
            _codeGeneratorLogFilePathLabel = new Label();
            _codeGeneratorLogFilePathLabel.Location = new Point(10, 150);
            _codeGeneratorLogFilePathLabel.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _codeGeneratorLogFilePathLabel.Text = "codeGeneratorLogFilePath";
            this.Controls.Add(_codeGeneratorLogFilePathLabel);

            _saveCodeGeneratorLogFileButton = new Button();
            _saveCodeGeneratorLogFileButton.Location = new Point(380, 180);
            _saveCodeGeneratorLogFileButton.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            _saveCodeGeneratorLogFileButton.Text = "Save";
            _saveCodeGeneratorLogFileButton.Click += new EventHandler(OnSaveCodeGeneratorLogButtonClick);
            this.Controls.Add(_saveCodeGeneratorLogFileButton);

            _codeGeneratorLogFilePathTextBox = new TextBox();
            _codeGeneratorLogFilePathTextBox.Location = new Point(10, 180);
            _codeGeneratorLogFilePathTextBox.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _codeGeneratorLogFilePathTextBox.Text = @"..\..\..\HookerRecord\bin\Debug\codeGeneratorLog.txt";
            this.Controls.Add(_codeGeneratorLogFilePathTextBox);

            //classCodeTemplate
            _classCodeTemplateFilePathLabel = new Label();
            _classCodeTemplateFilePathLabel.Location = new Point(10, 220);
            _classCodeTemplateFilePathLabel.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _classCodeTemplateFilePathLabel.Text = @"classCodeTemplateFilePath";
            this.Controls.Add(_classCodeTemplateFilePathLabel);

            _classCodeTemplateFilePathTextBox = new TextBox();
            _classCodeTemplateFilePathTextBox.Location = new Point(10, 250);
            _classCodeTemplateFilePathTextBox.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _classCodeTemplateFilePathTextBox.Text = @"..\..\..\HookerRecord\bin\Debug\classCodeTemplate.txt";
            this.Controls.Add(_classCodeTemplateFilePathTextBox);

            _browseClassCodeTemplateFileButton = new Button();
            _browseClassCodeTemplateFileButton.Location = new Point(380, 250);
            _browseClassCodeTemplateFileButton.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            _browseClassCodeTemplateFileButton.Text = "Browse";
            _browseClassCodeTemplateFileButton.Click += new EventHandler(OnBrowseButtonClick);
            this.Controls.Add(_browseClassCodeTemplateFileButton);

            //classCode
            _classCodeFilePathLabel = new Label();
            _classCodeFilePathLabel.Location = new Point(10, 300);
            _classCodeFilePathLabel.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _classCodeFilePathLabel.Text = "classCodeFilePath";
            this.Controls.Add(_classCodeFilePathLabel);

            _classCodeFilePathTextBox = new TextBox();
            _classCodeFilePathTextBox.Location = new Point(10, 330);
            _classCodeFilePathTextBox.Size = new Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
            _classCodeFilePathTextBox.Text = @"..\..\..\HookerRecord\bin\Debug\classCode.cs";
            this.Controls.Add(_classCodeFilePathTextBox);

            _saveClassCodeFileButton = new Button();
            _saveClassCodeFileButton.Location = new Point(380, 330);
            _saveClassCodeFileButton.Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT);
            _saveClassCodeFileButton.Text = "Save";
            _saveClassCodeFileButton.Click += new EventHandler(OnSaveClassCodeButtonClick);
            this.Controls.Add(_saveClassCodeFileButton);

            //Generate Button
            _generateClassCodeFileButton = new Button();
            _generateClassCodeFileButton.Location = new Point(100, 390);
            _generateClassCodeFileButton.Size = new Size(280, 40);
            _generateClassCodeFileButton.Text = "Generate Class Code";
            _generateClassCodeFileButton.Click += new EventHandler(OnGenerateButtonClick);
            this.Controls.Add(_generateClassCodeFileButton);
        }

        private void OnBrowseButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (sender.Equals(_browseRecorderLogFileButton)) _recorderLogFilePathTextBox.Text = openFileDialog.FileName;
                if (sender.Equals(_browseCodeGeneratorConfigurationFileButton)) _codeGeneratorConfigurationFilePathTextBox.Text = openFileDialog.FileName;
                if (sender.Equals(_browseClassCodeTemplateFileButton)) _classCodeTemplateFilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void OnSaveCodeGeneratorLogButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Log files | *.txt";
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _codeGeneratorLogFilePathTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void OnSaveClassCodeButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "C# code files | *.cs";
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                _codeGeneratorLogFilePathTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void OnGenerateButtonClick(object sender, EventArgs e)
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.PerformStandardCodeGeneration(_recorderLogFilePathTextBox.Text, _codeGeneratorConfigurationFilePathTextBox.Text, _codeGeneratorLogFilePathTextBox.Text, _classCodeTemplateFilePathTextBox.Text, _classCodeFilePathTextBox.Text);
            MessageBox.Show("Done");
        }
    }
}
