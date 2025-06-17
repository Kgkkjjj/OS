using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Text;

namespace CodeEditor
{
    public partial class MainWindow : Window
    {
        private string? currentFilePath;
        private readonly List<string> additionalReferences = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            Editor.Text = string.Empty;
            currentFilePath = null;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "C# Files (*.cs)|*.cs|All Files|*.*" };
            if (ofd.ShowDialog() == true)
            {
                currentFilePath = ofd.FileName;
                Editor.Text = File.ReadAllText(currentFilePath);
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAs_Click(sender, e);
            }
            else
            {
                File.WriteAllText(currentFilePath, Editor.Text);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "C# Files (*.cs)|*.cs|All Files|*.*" };
            if (sfd.ShowDialog() == true)
            {
                currentFilePath = sfd.FileName;
                File.WriteAllText(currentFilePath, Editor.Text);
            }
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            SaveFile_Click(sender, e);
            if (string.IsNullOrEmpty(currentFilePath))
                return;

            string exePath = Path.ChangeExtension(currentFilePath, "exe");
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = exePath
            };
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            foreach (var r in additionalReferences)
                parameters.ReferencedAssemblies.Add(r);

            CompilerResults result = provider.CompileAssemblyFromSource(parameters, Editor.Text);
            if (result.Errors.HasErrors)
            {
                var sb = new StringBuilder();
                foreach (CompilerError err in result.Errors)
                    sb.AppendLine(err.ToString());
                MessageBox.Show(sb.ToString(), "Build Errors");
            }
            else
            {
                MessageBox.Show($"Build succeeded: {exePath}");
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("Save and build the file first.");
                return;
            }

            var exePath = Path.ChangeExtension(currentFilePath, "exe");
            if (File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Executable not found. Build first.");
            }
        }

        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("Save and build the file first.");
                return;
            }

            var exePath = Path.ChangeExtension(currentFilePath, "exe");
            if (File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true, EnvironmentVariables = { ["COMPlus_DebugAttach"] = "1" } });
            }
            else
            {
                MessageBox.Show("Executable not found. Build first.");
            }
        }

        private void FormatCode_Click(object sender, RoutedEventArgs e)
        {
            var lines = Editor.Text.Split('\n');
            int indent = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (line.StartsWith("}"))
                    indent = Math.Max(0, indent - 1);
                lines[i] = new string(' ', indent * 4) + line;
                if (line.EndsWith("{"))
                    indent++;
            }
            Editor.Text = string.Join("\n", lines);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var query = Microsoft.VisualBasic.Interaction.InputBox("Search for:", "Search");
            if (!string.IsNullOrEmpty(query))
            {
                int index = Editor.Text.IndexOf(query, System.StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    Editor.Focus();
                    Editor.Select(index, query.Length);
                }
                else
                {
                    MessageBox.Show("Text not found.");
                }
            }
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            var search = Microsoft.VisualBasic.Interaction.InputBox("Search for:", "Replace");
            if (string.IsNullOrEmpty(search)) return;
            var replace = Microsoft.VisualBasic.Interaction.InputBox("Replace with:", "Replace");
            Editor.Text = Editor.Text.Replace(search, replace);
        }

        private void GitCommit_Click(object sender, RoutedEventArgs e)
        {
            var msg = Microsoft.VisualBasic.Interaction.InputBox("Commit message:", "Git Commit");
            if (!string.IsNullOrEmpty(msg))
            {
                RunProcess("git", $"add {currentFilePath ?? "."}");
                RunProcess("git", $"commit -m \"{msg.Replace("\"", "\\\"")}\"");
            }
        }

        private void GitPush_Click(object sender, RoutedEventArgs e)
        {
            RunProcess("git", "push");
        }

        private void AddReference_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "Assemblies (*.dll)|*.dll|All Files|*.*" };
            if (ofd.ShowDialog() == true)
            {
                additionalReferences.Add(ofd.FileName);
            }
        }

        private void ManageNuGet_Click(object sender, RoutedEventArgs e)
        {
            var pkg = Microsoft.VisualBasic.Interaction.InputBox("NuGet package:", "Manage NuGet");
            if (!string.IsNullOrEmpty(pkg))
            {
                RunProcess("dotnet", $"add package {pkg}");
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("No settings available yet.");
        }

        private static void RunProcess(string fileName, string args)
        {
            try
            {
                var psi = new ProcessStartInfo(fileName, args)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };
                var p = Process.Start(psi);
                p.WaitForExit();
                if (p.ExitCode != 0)
                    MessageBox.Show(p.StandardError.ReadToEnd(), fileName);
                else
                    MessageBox.Show(p.StandardOutput.ReadToEnd(), fileName);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, fileName);
            }
        }
    }
}
