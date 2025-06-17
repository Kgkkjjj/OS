using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Text;

namespace CodeEditor
{
    public partial class MainWindow : Window
    {
        private string? currentFilePath;
        private string? projectDirectory;
        private readonly List<string> additionalReferences = new();
        private readonly Dictionary<string, string> snippets = new();

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
                WriteOutput(sb.ToString());
            }
            else
            {
                WriteOutput($"Build succeeded: {exePath}");
            }
        }

        private void BuildAll_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(projectDirectory))
            {
                System.Windows.MessageBox.Show("Open a project first.");
                return;
            }

            var files = Directory.GetFiles(projectDirectory, "*.cs");
            if (files.Length == 0)
            {
                WriteOutput("No C# files found in project.");
                return;
            }

            string exePath = Path.Combine(projectDirectory, "Project.exe");
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

            CompilerResults result = provider.CompileAssemblyFromFile(parameters, files);
            if (result.Errors.HasErrors)
            {
                var sb = new StringBuilder();
                foreach (CompilerError err in result.Errors)
                    sb.AppendLine(err.ToString());
                WriteOutput(sb.ToString());
            }
            else
            {
                WriteOutput($"Build succeeded: {exePath}");
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                System.Windows.MessageBox.Show("Save and build the file first.");
                return;
            }

            var exePath = Path.ChangeExtension(currentFilePath, "exe");
            if (File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true });
                WriteOutput($"Running {exePath}");
            }
            else
            {
                System.Windows.MessageBox.Show("Executable not found. Build first.");
            }
        }

        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                System.Windows.MessageBox.Show("Save and build the file first.");
                return;
            }

            var exePath = Path.ChangeExtension(currentFilePath, "exe");
            if (File.Exists(exePath))
            {
                Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true, EnvironmentVariables = { ["COMPlus_DebugAttach"] = "1" } });
            }
            else
            {
                System.Windows.MessageBox.Show("Executable not found. Build first.");
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
                    System.Windows.MessageBox.Show("Text not found.");
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
                WriteOutput(RunProcess("git", $"add {currentFilePath ?? "."}").Trim());
                WriteOutput(RunProcess("git", $"commit -m \"{msg.Replace("\"", "\\\"")}\"").Trim());
            }
        }

        private void GitPush_Click(object sender, RoutedEventArgs e)
        {
            WriteOutput(RunProcess("git", "push").Trim());
        }

        private void AddReference_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "Assemblies (*.dll)|*.dll|All Files|*.*" };
            if (ofd.ShowDialog() == true)
            {
                additionalReferences.Add(ofd.FileName);
                WriteOutput($"Reference added: {ofd.FileName}");
            }
        }

        private void ManageNuGet_Click(object sender, RoutedEventArgs e)
        {
            var pkg = Microsoft.VisualBasic.Interaction.InputBox("NuGet package:", "Manage NuGet");
            if (!string.IsNullOrEmpty(pkg))
            {
                WriteOutput(RunProcess("dotnet", $"add package {pkg}").Trim());
            }
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                projectDirectory = fbd.SelectedPath;
                ProjectFiles.ItemsSource = Directory.GetFiles(projectDirectory, "*.cs");
                WriteOutput($"Project opened: {projectDirectory}");
            }
        }

        private void ProjectFiles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProjectFiles.SelectedItem is string path && File.Exists(path))
            {
                currentFilePath = path;
                Editor.Text = File.ReadAllText(path);
            }
        }

        private void Deploy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(projectDirectory))
            {
                System.Windows.MessageBox.Show("Open a project first.");
                return;
            }
            var exePath = Path.Combine(projectDirectory, "Project.exe");
            if (!File.Exists(exePath))
            {
                System.Windows.MessageBox.Show("Build project first.");
                return;
            }
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var dest = Path.Combine(fbd.SelectedPath, Path.GetFileName(exePath));
                File.Copy(exePath, dest, true);
                WriteOutput($"Deployed to {dest}");
            }
        }

        private void SnippetManager_Click(object sender, RoutedEventArgs e)
        {
            var name = Microsoft.VisualBasic.Interaction.InputBox("Snippet name:", "Snippets");
            if (string.IsNullOrEmpty(name))
                return;

            if (snippets.TryGetValue(name, out var code))
            {
                Editor.SelectedText = code;
            }
            else
            {
                var newCode = Microsoft.VisualBasic.Interaction.InputBox("Snippet code:", "Snippets");
                if (!string.IsNullOrEmpty(newCode))
                    snippets[name] = newCode;
            }
        }

        private void Terminal_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("cmd.exe") { UseShellExecute = true, WorkingDirectory = projectDirectory ?? System.Environment.CurrentDirectory });
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("No settings available yet.");
        }

        private static string RunProcess(string fileName, string args)
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
                var output = p.StandardOutput.ReadToEnd();
                var error = p.StandardError.ReadToEnd();
                p.WaitForExit();
                return string.IsNullOrEmpty(error) ? output : error;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        private void WriteOutput(string text)
        {
            Output.AppendText(text + System.Environment.NewLine);
            Output.ScrollToEnd();
        }
    }
}
