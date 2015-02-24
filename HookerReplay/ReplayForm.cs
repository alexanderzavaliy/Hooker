using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Hooker
{
    public partial class Replayer : Form
    {
        public Replayer()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Opacity = 0;

            ExecuteRecordedCode();
        }

        private void ExecuteRecordedCode()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
            {
                throw new Exception("Not enough parameters. Correct form: \"HookerReplay.exe <pathToClassCodeFile>\"");
            }

            Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v4.0"},
                };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;
            compilerParams.IncludeDebugInformation = false;

            compilerParams.ReferencedAssemblies.Add("System.dll");
            compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            compilerParams.ReferencedAssemblies.Add("System.Drawing.dll");

            CompilerResults results = provider.CompileAssemblyFromFile(compilerParams, args[1]);

            if (results.Errors.Count != 0)
            {
                foreach (CompilerError error in results.Errors)
                {
                    throw new Exception("Compile failed with error:" + error.ErrorText);
                }
            }

            object o = results.CompiledAssembly.CreateInstance("Hooker.Replayer");
            MethodInfo mi = o.GetType().GetMethod("ExecuteRecordedCode");
            mi.Invoke(o, null);
            Debug.WriteLine("Execution finished");

            Environment.Exit(0);
        }
    }
}
